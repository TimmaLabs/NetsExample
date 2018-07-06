using BBS.BAXI;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using NetsExample.Operations;
using NetsExample.Operations.Admin;
using NetsExample.Operations.Transactions;
using NetsExample.Terminal;

namespace NetsExample.Browser
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
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessPurchase(int amount, int VAT = 0, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Transaction t = null;

            if (VAT > 0)
            {
                t = new VATPurchase(amount, VAT, baxiArgs: opts.baxiArgs);
            }
            else
            {
                t = new Purchase(amount, baxiArgs: opts.baxiArgs);
            }

            terminalCtrl.SendTransactionOperation(t);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// Undo previous transaction
        /// </summary>
        /// <param name="amount">Amount of the previous transaction (must match!)</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessReversal(int amount, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Reversal t = new Reversal(amount, baxiArgs: opts.baxiArgs);
            terminalCtrl.SendTransactionOperation(t);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// Undo past transaction
        /// </summary>
        /// <param name="amount">Amount to return/refund</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string ProcessReturn(int amount, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Return t = new Return(amount, baxiArgs: opts.baxiArgs);
            terminalCtrl.SendTransactionOperation(t);
            return JsonConvert.SerializeObject(t.Args);
        }

        /// <summary>
        /// API (transaction operations) END
        /// </summary>


        /// <summary>
        /// API (admin operations) BEGIN
        /// </summary>

        /// <summary>
        /// Cancel an ongoing operation
        /// </summary>
        /// <param name="hard">
        ///     If set to false, the terminal has the choice to ignore the request.
        ///     The OnLocalMode result will determine the final status of the transaction.
        /// </param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        public void Cancel(bool hard = true, string options = "{}")
        {
            Debug.Write("Canceling");

            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);

            if (hard)
            {
                Debug.WriteLine(" hard...");
                HardCancel op = new HardCancel(baxiArgs: opts.baxiArgs);
                terminalCtrl.SendAdminOperation(op);
            }
            else
            {
                Debug.WriteLine(" soft...");
                SoftCancel op = new SoftCancel(baxiArgs: opts.baxiArgs);
                terminalCtrl.SendAdminOperation(op);
            }
        }

        /// <summary>
        /// Close the day & print the reconciliation report
        /// </summary>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string Reconcile(string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Reconciliation r = new Reconciliation(baxiArgs: opts.baxiArgs);
            terminalCtrl.SendAdminOperation(r);
            return JsonConvert.SerializeObject(r.Args);
        }

        /// <summary>
        /// Print various reports
        /// </summary>
        /// <param name="type">Report to print</param>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string PrintReport(string type, string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Operation<AdministrationArgs> op = null;

            switch (type.ToUpper())
            {
                case "Z":
                    op = new ZReport(baxiArgs: opts.baxiArgs);
                    break;
                case "X":
                    op = new XReport(baxiArgs: opts.baxiArgs);
                    break;
                case "EOT":
                    op = new EOTReport(baxiArgs: opts.baxiArgs);
                    break;
                case "LATEST":
                case "TRANSACTION":
                case "LATESTTRANSACTION":
                    op = new LatestTransactionReport(baxiArgs: opts.baxiArgs);
                    break;
                default:
                    throw new NotImplementedException(string.Format("Report of type {0} is not supported.", type));
            }

            Debug.WriteLine("Generating report of type {0}...", type);
            terminalCtrl.SendAdminOperation(op);
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

        /// <summary>
        /// Check/update the terminal software
        /// </summary>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string Update(string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            Update op = new Update(baxiArgs: opts.baxiArgs);
            terminalCtrl.SendAdminOperation(op);
            return JsonConvert.SerializeObject(op.Args);
        }

        /// <summary>
        /// Fetch card information for an account
        /// </summary>
        /// <param name="options">Options in JSON string format</param>
        /// <param name="options.baxiArgs">TransferAmount arguments in JSON string format</param>
        /// <returns>JSON payload sent to the terminal</returns>
        public string FetchCards(string options = "{}")
        {
            var opts = JsonConvert.DeserializeObject<OperationOptions>(options);
            CardData op = new CardData(baxiArgs: opts.baxiArgs);
            terminalCtrl.SendAdminOperation(op);
            return JsonConvert.SerializeObject(op.Args);
        }
        /// <summary>
        /// API (admin operations) END
        /// </summary>


        /// <summary>
        /// API helpers START
        /// </summary>

        /// <summary>
        /// Check if terminal connection has been opened
        /// </summary>
        public bool IsOpen()
        {
            return terminalCtrl.IsOpen();
        }

        /// <summary>
        /// Change terminal UI language (receipts will still use the card's language)
        /// </summary>
        /// <param name="langID">Language ID</param>
        /// <returns>MethodRejectCode</returns>
        public int ChangeLanguage(string langID = "en")
        {
            return terminalCtrl.SetLanguage(langID);
        }

        /// <summary>
        /// Ping the terminal host
        /// </summary>
        /// <returns>MethodRejectCode</returns>
        public int PingHost()
        {
            return terminalCtrl.PingHost();
        }

        public string OptionalData(string txnref = "", int autodcc = 1, int merch = -1)
        {
            return Utils.OptionalData(autodcc: autodcc, merch: merch, txnref: txnref);
        }

        public string GetVersion()
        {
            return App.Version;
        }

        public void OpenCashDrawer(string controlCodeJson)
        {
            byte[] controlCode = JsonConvert.DeserializeObject<byte[]>(controlCodeJson);
            Utils.OpenCashDrawer(controlCode);
        }

        public string GetTerminalInfo()
        {
            TerminalInfo info = terminalCtrl.GetInfo();
            return JsonConvert.SerializeObject(info);
        }
        /// <summary>
        /// API helpers END
        /// </summary>
    }
}
