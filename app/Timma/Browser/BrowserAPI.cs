using BBS.BAXI;
using Newtonsoft.Json;
using System;
using Timma.Operations;
using Timma.Operations.Admin;
using Timma.Operations.Transactions;

namespace Timma.Browser
{
    class BrowserAPI
    {
        private TerminalController terminalCtrl;

        public BrowserAPI(TerminalController terminalController)
        {
            terminalCtrl = terminalController;
        }

        /// <summary>
        /// API (transaction operations) START
        /// </summary>


        /// <summary>
        /// Process payment (VAT or VATless)
        /// </summary>
        /// <param name="amount">Amount to charge (in cents)</param>
        /// <param name="VAT">Amount of VAT</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.printText">
        ///     Custom print text, use the {{baxiTxt}} placeholder tag
        ///     to embed the Baxi print: { type: 'txt', data: '{{baxiTxt}}' }
        /// </param>
        /// <param name="options.payload">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessPurchase(int amount, int VAT = 0, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Transaction t = null;

            if (VAT > 0)
            {
                t = new VATPurchase(amount, VAT, printText: opts.printText, payload: opts.payload);
            }
            else
            {
                t = new Purchase(amount, printText: opts.printText, payload: opts.payload);
            }

            terminalCtrl.SendTransactionOperation(t, print: true);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// Undo previous transaction
        /// </summary>
        /// <param name="amount">Amount of the previous transaction (must match!)</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.printText">
        ///     Custom print text, use the {{baxiTxt}} placeholder tag
        ///     to embed the Baxi print: { type: 'txt', data: '{{baxiTxt}}' }
        /// </param>
        /// <param name="options.payload">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessReversal(int amount, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Reversal t = new Reversal(amount, printText: opts.printText, payload: opts.payload);
            terminalCtrl.SendTransactionOperation(t, print: true);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// Undo past transaction
        /// </summary>
        /// <param name="amount">Amount to return/refund</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.printText">
        ///     Custom print text, use the {{baxiTxt}} placeholder tag
        ///     to embed the Baxi print: { type: 'txt', data: '{{baxiTxt}}' }
        /// </param>
        /// <param name="options.payload">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessReturn(int amount, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Return t = new Return(amount, printText: opts.printText, payload: opts.payload);
            terminalCtrl.SendTransactionOperation(t, print: true);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// API (transaction operations) END
        /// </summary>


        /// <summary>
        /// API (admin operations) BEGIN
        /// </summary>

        /// <summary>
        /// Cancel ongoing operation
        /// </summary>
        /// <param name="hard">
        ///         Whether or not to prompt the user for cancellation
        /// (does seem to be supported by the Baxi API / terminal, though...)
        /// </param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.printText">
        ///     Custom print text, use the {{baxiTxt}} placeholder tag
        ///     to embed the Baxi print: { type: 'txt', data: '{{baxiTxt}}' }
        /// </param>
        /// <param name="options.payload">TransferAmount arguments in JSON string format</param>
        public void Cancel(bool hard = true, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Console.Write("Canceling");

            if (hard)
            {
                Console.WriteLine(" hard...");
                HardCancel op = new HardCancel(printText: opts.printText);
                terminalCtrl.SendAdminOperation(op);
            }
            else
            {
                Console.WriteLine(" soft...");
                SoftCancel op = new SoftCancel(printText: opts.printText);
                terminalCtrl.SendAdminOperation(op);
            }
        }

        /// <summary>
        /// Close for the day & print EOD report
        /// </summary>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.printText">
        ///     Custom print text, use the {{baxiTxt}} placeholder tag
        ///     to embed the Baxi print: { type: 'txt', data: '{{baxiTxt}}' }
        /// </param>
        /// <param name="options.payload">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string Reconcile(string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Reconciliation r = new Reconciliation(printText: opts.printText, payload: opts.payload);
            terminalCtrl.SendAdminOperation(r, print: true);
            return JsonConvert.SerializeObject(r.Args);
        }

        /// <summary>
        /// Print various reports
        /// </summary>
        /// <param name="type">Report to print</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.printText">
        ///     Custom print text, use the {{baxiTxt}} placeholder tag
        ///     to embed the Baxi print: { type: 'txt', data: '{{baxiTxt}}' }
        /// </param>
        /// <param name="options.payload">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string PrintReport(string type, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Operation<AdministrationArgs> op = null;

            switch (type.ToUpper())
            {
                case "Z":
                    op = new ZReport(printText: opts.printText);
                    break;
                case "X":
                    op = new XReport(printText: opts.printText);
                    break;
                case "EOT":
                    op = new EOTReport(printText: opts.printText);
                    break;
                case "LATEST":
                case "TRANSACTION":
                case "LATESTTRANSACTION":
                    op = new LatestTransactionReport(printText: opts.printText);
                    break;
                default:
                    throw new NotImplementedException(string.Format("Report of type {0} is not supported.", type));
            }

            Console.WriteLine("Generating report of type {0}...", type);
            terminalCtrl.SendAdminOperation(op, print: true);
            return JsonConvert.SerializeObject(op.Args);
        }

        /// <summary>
        /// Raw print
        /// </summary>
        /// <param name="printText">
        ///     Document to print (must be in the appropriate Baxi 
        ///     JSON print format, see BAXI.Net Programmers Guide)
        /// </param>
        /// <returns>Success code (1 = success, 0 = fail)</returns>
        public int Print(string printText)
        {
            return terminalCtrl.Print(printText);
        }

        public string OptionalData(string txnref = "", int autodcc = 1, int merch = -1)
        {
            return Utils.OptionalData(autodcc: autodcc, merch: merch, txnref: txnref);
        }

        /// <summary>
        /// Change terminal UI language (receipts will still use the card's language)
        /// </summary>
        /// <param name="langID">Language ID</param>
        /// <returns>Success code (1 = success, 0 = fail)</returns>
        public int ChangeLanguage(string langID = "en")
        {
            return terminalCtrl.SetLanguage(langID);
        }

        /// <summary>
        /// API (admin operations) END
        /// </summary>
    }
}
