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
        /// <param name="payload">Custom JSON payload to be sent to the terminal</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessPurchase(int amount, int VAT = 0, string printText = "", string payload = "{}")
        {
            Transaction t = null;

            if (VAT > 0)
            {
                t = new VATPurchase(amount, VAT, printText: printText, payload: payload);
            }
            else
            {
                t = new Purchase(amount, printText: printText, payload: payload);
            }

            terminalCtrl.SendTransactionOperation(t, print: true);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// Undo previous transaction
        /// </summary>
        /// <param name="amount">Amount of the previous transaction (must match!)</param>
        /// <param name="payload">Custom JSON payload to be sent to the terminal</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessReversal(int amount, string printText = "", string payload = "{}")
        {
            Reversal t = new Reversal(amount, printText: printText, payload: payload);
            terminalCtrl.SendTransactionOperation(t, print: true);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// Undo past transaction
        /// </summary>
        /// <param name="amount">Amount to return/refund</param>
        /// <param name="payload"></param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessReturn(int amount, string printText = "", string payload = "{}")
        {
            Return t = new Return(amount, printText: printText, payload: payload);
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
        /// Whether or not to prompt the user for cancellation
        /// (does seem to be supported by the Baxi API / terminal, though...)
        /// </param>
        public void Cancel(bool hard = true, string printText = "")
        {
            Console.Write("Canceling");
            if (hard)
            {
                Console.WriteLine(" hard...");
                HardCancel op = new HardCancel(printText: printText);
                terminalCtrl.SendAdminOperation(op);
            }
            else
            {
                Console.WriteLine(" soft...");
                SoftCancel op = new SoftCancel(printText: printText);
                terminalCtrl.SendAdminOperation(op);
            }
        }

        /// <summary>
        /// Close for the day & print EOD report
        /// </summary>
        /// <param name="payload">Custom JSON payload to be sent to the terminal</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string Reconcile(string printText = "", string payload = "{}")
        {
            Reconciliation r = new Reconciliation(printText: printText, payload: payload);
            terminalCtrl.SendAdminOperation(r, print: true);
            return JsonConvert.SerializeObject(r.Args);
        }

        /// <summary>
        /// Print various reports
        /// </summary>
        /// <param name="type">Report to print</param>
        /// <returns>>JSON payload sent to the terminal</returns>
        public string PrintReport(string type, string printText = "")
        {
            Operation<AdministrationArgs> op = null;

            switch (type.ToUpper())
            {
                case "Z":
                    op = new ZReport(printText: printText);
                    break;
                case "X":
                    op = new XReport(printText: printText);
                    break;
                case "EOT":
                    op = new EOTReport(printText: printText);
                    break;
                case "LATEST":
                case "TRANSACTION":
                case "LATESTTRANSACTION":
                    op = new LatestTransactionReport(printText: printText);
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
        /// Document to print (must be in the appropriate Baxi 
        /// JSON print format, see BAXI.Net Programmers Guide)
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
