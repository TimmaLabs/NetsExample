var terminal = __TimmaECR;

/**
 * Event listeners (overridable, defaults to Timma.Browser.BrowserController.JSLogFunction)
 */

//
/**
 * Triggered when the browser frame has been loaded and is ready for interaction
 */
terminal.onLoaded = function () {}

//
/**
 * Triggered when the terminal operation was successful
 * @param  {Object} localModeEventArgs
 */
terminal.onSuccess = function (localModeEventArgs) {}

/**
 * Triggered when the terminal operation was unsuccessful (payload: message, code, codeParent)
 * @param {Object} errorPayload
 * @param {String} errorPayload.message     Error message
 * @param {Number} errorPayload.code        Error code
 * @param {Number} errorPayload.codeParent  Parent error code
 */
terminal.onError = function (errorPayload) {}

/**
 * Triggered when the terminal is ready to receive the next operation.
 * onSuccess and onError events are always followed by an onReady event.
 */
terminal.onReady = function () {}

/**
 * Triggered when the terminal has generated print text for an operation
 * @param  {Object} printTextEventArgs
 */
terminal.onPrint = function (printTextEventArgs) {}

/**
 * Triggered when text is displayed on the terminal screen
 * @param  {Object} displayTextEventArgs
 */
terminal.onDisplay = function (displayTextEventArgs) {}


/**
 * Below 'operation' refers to either a transactional or an administrative operation.
 */

/*
 * Transaction
 */
terminal.processPurchase(1500); // 15€ purchase
terminal.processPurchase(1500, 400); // 15€ purchase with 4€ VAT
terminal.processPurchase(1500, 400, JSON.stringify({ printText: foobar }) ); // ... with custom print text (stored in variable `foobar`, see Baxi API for the valid print text format)
terminal.processPurchase(1500, 400, JSON.stringify({ baxiArgs: JSON.stringify({ Amount2: 1234 }) }) ); // ... with custom Baxi arguments (see Baxi API docs for the valid keys/fields)

terminal.processReversal(1500); // Reverse/undo previous transaction
terminal.processReversal(1500, JSON.stringify({ printText: foobar }) ); // ... with custom print text
terminal.processReversal(1500, JSON.stringify({ baxiArgs: JSON.stringify({ Type2: 0x31 }) }) ); // ... with custom Baxi arguments

terminal.processReturn(2000); // Refund 20€
terminal.processReturn(2000, JSON.stringify({ printText: foobar }) ); // ... with custom print text
terminal.processReturn(2000, JSON.stringify({ baxiArgs: JSON.stringify({ AuthCode: 567 }) }) ); // ... with custom Baxi arguments


/*
 * Administration
 */
terminal.cancel(); // (Hard) Cancel a pending operation
terminal.cancel(2000, JSON.stringify({ printText: foobar }) ); // ... with custom print text
terminal.cancel(false); // Soft cancel a pending operation (soft, as in, ask the user first. The test terminal doesn't seem to support this, though...)

terminal.reconcile(); // AKA "päivänpäätös"
terminal.reconcile(JSON.stringify({ printText: foobar }) ); // ... with custom print text
terminal.reconcile(JSON.stringify({ baxiArgs: JSON.stringify({ Amount2: 1234 }) }) ); // ... with custom Baxi arguments

terminal.printReport('x') // Print X-report (see app/Timma/Browser/BrowserAPI.cs in TimmaLabs/TimmaNets for all the supported report types)
terminal.printReport('x', JSON.stringify({ printText: foobar }) ); // ... with custom print text

terminal.print( JSON.stringify({ printmsg: { ver:'1.0', rows: [{ type:'txt', data: 'foo' },{ type: 'txt', blank: 15 }] } }) ); // Print custom receipt (see Baxi API docs for valid print payload format)

terminal.changeLanguage('se'); // Change terminal language to Swedish
terminal.changeLanguage(); // Change terminal language to English (default)

/**
 * Helpers
 */
terminal.optionalData('txnref-goes-here', 1, 567); // Output OptionalData structure including the available fields (txnref, autodcc, merch)
terminal.optionalData('txnref-goes-here');
terminal.optionalData('', 2);

terminal.isPrinting(); // Check if the terminal is currently printing / about to print
