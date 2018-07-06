```js
var terminal = window.NetsExample;

/**
 * EVENT LISTENERS (overridable, defaults to NetsExample.Browser.BrowserController.JSLogFunction)
 */

/**
 * Triggered when the browser frame has been loaded and is ready for interaction
 */
terminal.onLoaded = function () {}

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
 * Triggered when the terminal has generated print text for an operation.
 * @param  {Object} printTextEventArgs
 */
terminal.onPrint = function (printTextEventArgs) {}

/**
 * Triggered when text is displayed on the terminal screen.
 * @param  {Object} displayTextEventArgs
 */
terminal.onDisplay = function (displayTextEventArgs) {}


/**
 * Below 'operation' refers to either a transactional or an administrative operation.
 */

/*
 * TRANSACTION
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
 * ADMINISTRATION
 */
terminal.cancel(); // (Hard) Cancel a pending operation
terminal.cancel(false); // Soft cancel a pending operation (soft, as in, ask the user first. The test terminal doesn't seem to support this, though...)

terminal.reconcile(); // AKA "päivänpäätös"
terminal.reconcile(JSON.stringify({ printText: foobar }) ); // ... with custom print text
terminal.reconcile(JSON.stringify({ baxiArgs: JSON.stringify({ OptionalData: JSON.stringify({}) }) }) ); // ... with custom Baxi arguments

// Print custom receipt (see Baxi API docs for valid print payload format)
terminal.print( JSON.stringify({ printmsg: { ver:'1.0', rows: [{ type:'txt', data: 'foo' }, { type: 'txt', blank: 15 }] } }) );

terminal.printReport('x') // Print X-report (see app/src/Browser/BrowserAPI.cs)
terminal.printReport('x', JSON.stringify({ printText: foobar }) ); // ... with custom print text (stored in variable `foobar`)

/**
 * Utilities
 */
terminal.changeLanguage('se'); // Change terminal language to Swedish
terminal.changeLanguage(); // Change terminal language to English (default)

// Output OptionalData structure including the available fields (txnref, autodcc, merch)
terminal.optionalData('txnref-goes-here', 1, 567);
terminal.optionalData('txnref-goes-here');
terminal.optionalData('', 2);

terminal.update(); // Check/update the terminal software
terminal.fetchCards(); // Fetch card information for an account
terminal.pingHost(); // Send a ping to the host (Nets server)

terminal.getVersion(); // Get POS application version
terminal.getTerminalInfo(); // Get terminal information (ID, type, baud rate...)
terminal.isOpen(); // Test POS connection (USB)
terminal.log(message, prefix, color); // Log a (pre-formatted) message to the browser console
terminal.openCashDrawer(JSON.stringify([ 7 ])); // Open a cash drawer via receipt printer using a predefined control code
```
