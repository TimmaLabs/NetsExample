using System;
using System.Text;
using BBS.BAXI;
using Timma.Operations;
using System.Diagnostics;

namespace Timma
{
    class TerminalController
    {
        private BaxiCtrl _terminal;

        private bool opening = false;
        char ASCII_REPORT_SEPARATOR = Convert.ToChar(30);
        char ASCII_UNIT_SEPARATOR = Convert.ToChar(31);

        private bool _printing { get; set; }

        public TerminalController(BaxiCtrl terminal)
        {
            _terminal = terminal;
        }

        public void Initialize()
        {
            _terminal.OnLocalMode += HandleLocalMode;
            _terminal.OnError += HandleError;
            _terminal.OnPrintText += HandlePrintText;
            _terminal.OnTerminalReady += HandleReady;
            _terminal.OnJsonReceived += HandleJsonReceived;

            Open();
        }

        internal int SendTransactionOperation(Operation<TransferAmountArgs> op, bool print = false)
        {
            if (print)
            {
                PreparePrintOperation(op);
            }
            return _terminal.TransferAmount(op.Args);
        }

        internal int SendAdminOperation(Operation<AdministrationArgs> op, bool print = false)
        {
            if (print)
            {
                PreparePrintOperation(op);
            }
            return _terminal.Administration(op.Args);
        }

        private void PreparePrintOperation(IOperation op)
        {
            PrintTextHandler _OnPrintText = delegate { };
            ReadyHandler _OnReady = delegate { };
            string printText = string.Empty;

            _OnPrintText = (sender, args) =>
            {
                OnPrintText -= _OnPrintText;
                Debug.WriteLine("PRINT TEXT CB CALLED!");

                printText = args.PrintText;
                _printing = true;
            };

            _OnReady = (sender, args) =>
            {
                OnReady -= _OnReady;
                Debug.WriteLine("TERMINAL READY CB CALLED!");

                if (string.IsNullOrWhiteSpace(printText))
                {
                    Debug.WriteLine("Nothing to print!");
                }
                else
                {
                    Print(op.GenerateDocument(printText));
                }

                _printing = false;
            };

            OnPrintText += _OnPrintText;
            OnReady += _OnReady;
        }

        public int Print(string jsonStr)
        {
            var json = new SendJsonArgs(jsonStr);
            int code = _terminal.SendJson(json);
            Debug.WriteLine("SendJson return content {0}:", json.JsonData);
            Debug.WriteLine("SendJson return code: {0}", code);
            return code;
        }

        public int SetLanguage(String langID = "en")
        {
            SendTldArgs args = new SendTldArgs();
            args.TldType = "CMD";
            args.TldField = Encoding.ASCII.GetBytes("1014" + ASCII_UNIT_SEPARATOR + "0002" + ASCII_UNIT_SEPARATOR + langID + ASCII_REPORT_SEPARATOR);

            Debug.WriteLine("Changing language to {0}", langID);
            int code =  _terminal.SendTLD(args);
            return Convert.ToBoolean(code) ? code : _terminal.MethodRejectCode;
        }

        public bool IsPrinting()
        {
            return _printing;
        }

        public void Open()
        {
            opening = true;

            Debug.WriteLine("Opening terminal...");
            int code = _terminal.Open();

            if (code == 0)
            {
                int errorCode = _terminal.MethodRejectCode;
                opening = false;
                OnError("Terminal failed to be opened", errorCode, code);
            }
        }

        public void Close()
        {
            Debug.WriteLine("Closing terminal...");
            _terminal.Close();
        }

        public bool CanOpen()
        {
            Debug.WriteLine("TERMINAL OPEN {0}; OPENING TERMINAL: {1}", _terminal.IsOpen(), opening);
            // TODO: IsOpen() return true even if USB has gotten USB disconnected
            return !_terminal.IsOpen() && !opening;
        }

        public delegate void SuccessHandler(object sender, LocalModeEventArgs args);
        public event SuccessHandler OnSuccess;

        public delegate void ReadyHandler(object sender, TerminalReadyEventArgs args);
        public event ReadyHandler OnReady = delegate { };

        public delegate void ErrorHandler(string errorMessage, int errorCode, int errorCodeParent);
        public event ErrorHandler OnError;

        public delegate void PrintTextHandler(object sender, PrintTextEventArgs args);
        public event PrintTextHandler OnPrintText = delegate { };

        public delegate void OpenHandler(object sender, LocalModeEventArgs args);
        public event OpenHandler OnOpen;

        private void HandleLocalMode(object sender, LocalModeEventArgs args)
        {
            // Unknown result: will be taken care of by the error handler (HandleError)
            if (args.Result == 99) { return; }

            if (args.Result == 2)
            {
                OnError("Rejected", args.AccumulatorUpdate, args.Result);
                return;
            }

            OnSuccess(sender, args);

            if (opening)
            {
                opening = false;
                OnOpen(sender, args);
            }
        }

        private void HandleReady(object sender, TerminalReadyEventArgs args)
        {
            OnReady(sender, args);
        }

        private void HandleError(object sender, BaxiErrorEventArgs args)
        {
            Debug.WriteLine("Error code: {0}; message: {1}", args.ErrorCode, args.ErrorString);
            opening = false;
            OnError(args.ErrorString, args.ErrorCode, -1);
        }

        private void HandlePrintText(object sender, PrintTextEventArgs args)
        {
            OnPrintText(sender, args);
        }

        private void HandleJsonReceived(object sender, JsonReceivedArgs args)
        {
            Console.Write("JSON received: {0}", args.JsonString);
        }
    }
}
