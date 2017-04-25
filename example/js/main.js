$(function () {

  var terminal = window.__NetsTerminal || {}
  var SUPPORTED_TERMINAL_TYPES = { iCT250: '34', iPP350: '32' }
  var $display = $('.js-pos-example__output-view-term-display')
  var $log = $('.js-pos-example__output-view-log-content')
  var isIPP350 = false
  var outputViewOpenedAtLeastOnce = false
  var processing = false
  var logs = []
  var printRows = []

  if (isEmptyObject(terminal)) {
    console.info('POS environment not available. Please run the application in .NET context.')
    init()
  }

  terminal.onLoaded = function handleOnLoaded () {
    return terminal.getTerminalInfo().then(function (infoStr) {
      var info = JSON.parse(infoStr)
      var type = getTerminalTypeByCode(info.type)

      var html = Object.keys(info).map(function (key) {
        var val = key === 'type' ? type : info[key]
        return `
          <div class="row">
            <dt class="col-xs-5 pos-example__term-info-item-key">${key}</dt>
            <dd class="col-xs-7 pos-example__term-info-item-val">${val}</dd>
          </div>
        `.trim()
      })

      $display.html(`
        <dl class="pos-example__term-info">
          <h5 class="pos-example__term-info-title">Terminal info</h3>
          ${html.join('\n')}
        </dl>
      `.trim())

      $('.js-pos-example__output-view-term-model').text(type || 'Terminal disconnected!')

      if (SUPPORTED_TERMINAL_TYPES.iPP350 === info.type.slice(0, 2)) {
        $('input[name="reports"][value="custom"]').attr('disabled', true)
        isIPP350 = true
      }
    }).then(init)
  }

  function init () {
    $('input[type="radio"]').on('click', function (e) {
      var $input = $(e.currentTarget)
      var group = $input.attr('name')
      var val = $input.val()

      switch (group) {
        case 'reports':
          handleReport(val, $input)
          break
        case 'utilities':
          var changeLanguage = val === 'change-language'
          var displayOptionalData = val === 'optional-data'
          $('.js-process-utility-btn').toggle(!changeLanguage)
          $('.js-pos-example__utilities-language-container').toggleClass('hidden', !changeLanguage)
          $('.js-pos-example__utilities-optional-data-container').toggleClass('hidden', !displayOptionalData)
          break
        default:

      }
    })

    $('input[name="languages"]').on('click', function (e) {
      var $input = $(e.currentTarget)
      var language = $input.val()
      language && terminal.changeLanguage(language)
    })

    $('body').on('click', 'a', function (e) {
      var $link = $(e.currentTarget)
      var section = ($link.attr('href') || '').slice(1)

      e.preventDefault()

      switch (section) {
        case 'output':
          outputViewOpenedAtLeastOnce = true
          toggleOutputView()
          break
        case 'purchase':
          process(() => { return makePurchase() })
          break
        case 'reverse':
          process(() => { return reversePurchase() })
          break
        case 'refund':
          process(() => { return refundPurchase() })
          break
        case 'cancel':
          process(() => { return cancel() }, { immediate: true })
          break
        case 'reconcile':
          process(() => { return reconcile() })
          break
        case 'print':
          process(() => { return print() })
          break
        case 'process-utility':
          var $input = $('input[name="utilities"]:checked')
          handleUtilities($input.val(), $input)
          break
      }
    })

    $('#merchant-id').on('change', (e) => {
      var $textarea = $('.js-report-custom-value')
      var merchantId = $(e.currentTarget).val()
      var printmsgStr = $textarea.val()
      try {
        obj = JSON.parse(printmsgStr)
        obj.printmsg = obj.printmsg || {}
        obj.o = obj.o || {}
        obj.o.merch = merchantId ? parseInt(merchantId, 10) : null
        $textarea.val(JSON.stringify(obj, null, 4))
      } catch (e) {}
    })

    terminal.onDisplay = function (payload) {
      $display.html(payload.DisplayText)
      addLog(payload.DisplayText)
      terminal.log(payload, 'onDisplay', 'orange')
    }

    terminal.onPrint = function (payload) {
      $('.pos-example__output-view-term-print-txt').html(`<pre>${payload.PrintText}</pre>`)
      payload && printRows.push(payload.PrintText)
      terminal.log(payload, 'onPrint')
    }

    terminal.onSuccess = function (payload) {
      addLog(payload.TruncatedPan || 'Success!', 'success')
      terminal.log(payload, 'onSuccess', 'green')

      if (printRows.length) {
        isIPP350 ? sendToPrinter(printRows.slice()) : _printCustom(preparePrintObj(printRows.slice()))
      }

      printRows = []
    }

    terminal.onError = function (payload) {
      addLog(payload.message, 'error')
      terminal.log(payload, 'onError', 'red')
      setProcessing(false)
    }

    terminal.onReady = function (payload) {
      terminal.log(payload, 'onReady', '#0091ff')
      addLog(payload || 'Terminal ready!', 'ready')
      setProcessing(false)
    }
  }

  function preparePrintObj (printRows) {
    var rows = printRows.reduce((rows, txt) => {
      return rows.concat([ { type:'txt', data: txt }, { type: 'txt', blank: 15 } ])
    }, [])

    var payload = { printmsg: { ver: '1.00', rows: rows } }
    var merchId = $('#merchant-id').val()

    if (merchId) {
      payload.o = { ver: '1.00', merch: parseInt(merchId, 10) }
    }

    return payload
  }

  function isEmptyObject (obj) { return !(obj && Object.keys(obj).length) }

  function addLog (message, type) {
    var timestamp = (new Date()).toLocaleTimeString()

    logs.unshift(`<span class="log-${type || 'default'}">${timestamp} ${message}</span>`)

    var rows = logs.slice(0, 15)

    if (rows.join('').length > 720) {
      rows = logs.slice(0, 2)
      logs = logs.slice(0, 2)
    }

    if (!outputViewOpenedAtLeastOnce && !outputViewIsOpen()) {
      $('.js-pos-example__output-link').animateCss('rubberBand')
    }

    var logHtml = rows.reverse().join('<br />')
    $log.html(logHtml)
  }

  function sendToPrinter (content) {
    var elementId = 'pos-example__print-area'
    var printArea = document.getElementById(elementId)

    if (!printArea) {
      printArea = document.createElement('iframe')
      printArea.id = elementId
      printArea.className = 'hidden'
      printArea.style.width = printArea.style.height = '0px'
      document.body.appendChild(printArea)
    }
    var printWindow = printArea.contentWindow

    content = content.map(txt => `<pre>${txt}</pre>`)
    printWindow.document.open()
    printWindow.document.write(content.join('<br>'))
    printWindow.document.close()
    printWindow.focus()
    printWindow.print()
  }

  function setProcessing (val) {
    processing = val
    $('.js-pos-example__process-indicator').toggleClass('processing', val)
  }

  function outputViewIsOpen () {
    return $('body').hasClass('js-pos-example__body__output-view-is-open')
  }

  function process (operation, options = {}) {
    if (processing && !options.immediate) return

    try {
      setProcessing(true)
      operation().catch(e => {
        throw e
      })
    } catch (e) {
      setProcessing(false)
      console.error(e.message)
    }
  }

  function handleReport (type, $el) {
    var $printArea = $('.js-pos-example__print-custom-container')
    $printArea.toggleClass('hidden', type !== 'custom')
  }

  function handleUtilities (val, $el) {

    var handleResponse = function (promise) {
      return promise.then(response => {
        addLog(response, 'success')
      }).catch(response => {
        addLog(response, 'error')
      })
    }

    switch (val) {
      case 'is-open':
        var promise = terminal.isOpen()
        handleResponse(promise)
        break
      case 'ping-host':
        var promise = terminal.pingHost()
        process(() => { return handleResponse(promise) })
        break
      case 'get-version':
        var promise = terminal.getVersion()
        handleResponse(promise)
        break
      case 'get-info':
        var promise = terminal.getTerminalInfo().then(resp => {
          return `<br>${JSON.stringify(JSON.parse(resp), null, ' ')}`
        })
        handleResponse(promise)
        break
      case 'optional-data':
        var txnref = $('#txnref').val() || ''
        var autodcc = parseInt($('#autodcc').val() || $('#autodcc').prop('placeholder'), 10)
        var merch = parseInt($('#merch').val() === '' ? -1 : $('#merch').val(), 10)
        var promise = terminal.optionalData(txnref, autodcc, merch).then(resp => `<br>${resp}`)

        handleResponse(promise)
        break
      case 'fetch-account':
        process(() => { return terminal.fetchCards() })
        break
      case 'update-terminal':
        process(() => { return terminal.update() })
        break
    }
  }

  function getTerminalTypeByCode (typeCode) {
    typeCode = typeCode.slice(0, 2)
    return Object.keys(SUPPORTED_TERMINAL_TYPES).reduce(function (found, type) {
      return found || SUPPORTED_TERMINAL_TYPES[type] == typeCode && type
    }, null)
  }

  function processAmount ($el) {
    var amountStr = parseFloat($el.val() || 0, 10)
    return amountStr * 100
  }

  function makePurchase () {
    var vat = processAmount($('#purchase-vat'))
    var amount = processAmount($('#purchase-amount'))
    var options = prepareOptions()
    return terminal.processPurchase(amount, vat, options)
  }

  function reversePurchase () {
    var amount = processAmount($('#purchase-amount'))
    var options = prepareOptions()
    return terminal.processReversal(amount, options)
  }

  function refundPurchase () {
    var amount = processAmount($('#refund-amount'))
    var options = prepareOptions()
    return terminal.processReturn(amount, options)
  }

  function cancel () {
    return terminal.cancel()
  }

  function reconcile () {
    var options = prepareOptions()
    return terminal.reconcile(options)
  }

  function print () {
    var type = $('input[name="reports"]:checked').val()
    printRows = []

    if (type === 'custom') {
      var printJson = $('.js-report-custom-value').val()
      return _printCustom(printJson).catch(e => {
        if (e instanceof SyntaxError) {
          console.error('Invalid print JSON payload:\n\n ' + printJson)
          addLog('Invalid print JSON payload', 'error')
        }
      })
    }

    var options = prepareOptions()
    return terminal.printReport(type, options)
  }

  function _printCustom (printJson) {
    return new Promise(function (resolve, reject) {
      try {
        var printmsg = typeof printJson === 'string' ? JSON.parse(printJson) : printJson // validate
        return terminal.print(JSON.stringify(printmsg)).then(resolve).catch(reject)
      }
      catch (error) {
        setProcessing(false)
        reject(error)
      }
    })
  }

  function toggleOutputView () {
    $('.js-pos-example__output-link-indicator').toggleClass('rotate-180')
    $('.js-pos-example__output-view-container').toggleClass('no-margin')
    $('body').toggleClass('js-pos-example__body__output-view-is-open')
  }

  function prepareOptions (options) {
    options = options || {}
    var stringify = JSON.stringify
    var opts = {
      printText: options.printText || {},
      baxiArgs: options.baxiArgs || {},
    }

    opts.baxiArgs.OperID = opts.baxiArgs.OperID || $('#oper-id').val() || '0000'
    opts.printText = isEmptyObject(opts.printText) ? '' : stringify(opts.printText)
    opts.baxiArgs = isEmptyObject(opts.baxiArgs) ? '' : opts.baxiArgs

    if (opts.baxiArgs) {
      var optionalData = isEmptyObject(opts.baxiArgs.OptionalData) ? '' : stringify(opts.baxiArgs.OptionalData)
      opts.baxiArgs.OptionalData = optionalData
      opts.baxiArgs = stringify(opts.baxiArgs)
    }
    return JSON.stringify(opts)
  }
})

$.fn.extend({
  animateCss: function (animationName) {
    var animationEnd = 'webkitAnimationEnd animationend'
    this.addClass('animated ' + animationName).one(animationEnd, function () {
      $(this).removeClass('animated ' + animationName)
    })
  }
})
