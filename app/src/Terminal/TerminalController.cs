using System;
using System.Text;
using BBS.BAXI;
using NetsExample.Operations;
using System.Diagnostics;

namespace NetsExample.Terminal
{
    class TerminalController
    {
        private BaxiCtrl _terminal;

        private bool opening = false;
        char ASCII_REPORT_SEPARATOR = Convert.ToChar(30);
        char ASCII_UNIT_SEPARATOR = Convert.ToChar(31);

        private string HostIpAddress
        {
            get
            {
                #if (PRODUCTION && !DEBUG)
                return "91.102.24.142"; // Nets production server
                #else
                return "91.102.24.111"; // Nets test server
                #endif
            }
        }

        public TerminalController(BaxiCtrl terminal)
        {
            _terminal = terminal;
        }

        public void Initialize()
        {
            // Handlers
            _terminal.OnLocalMode += HandleLocalMode;
            _terminal.OnError += HandleError;
            _terminal.OnPrintText += HandlePrintText;
            _terminal.OnDisplayText += HandleDisplayText;
            _terminal.OnTerminalReady += HandleReady;
            _terminal.OnJsonReceived += HandleJsonReceived;

            // Baxi.ini settings (see baxi.ini for defaults)
            _terminal.HostIpAddress = HostIpAddress;
            _terminal.UseExtendedLocalMode = 1;
        }

        internal int SendTransactionOperation(Operation<TransferAmountArgs> op)
        {
            return _terminal.TransferAmount(op.Args);
        }

        internal int SendAdminOperation(Operation<AdministrationArgs> op)
        {
            return _terminal.Administration(op.Args);
        }

        public int Print(string jsonStr)
        {
            var json = new SendJsonArgs(jsonStr);
            int code = _terminal.SendJson(json);
            Debug.WriteLine("SendJson return content {0}:", json.JsonData);
            Debug.WriteLine("SendJson reject code: {0}", _terminal.MethodRejectCode);
            return _terminal.MethodRejectCode;
        }

        public int SetLanguage(String langID = "en")
        {
            SendTldArgs args = new SendTldArgs();
            args.TldType = "CMD";
            args.TldField = Encoding.ASCII.GetBytes("1014" + ASCII_UNIT_SEPARATOR + "0002" + ASCII_UNIT_SEPARATOR + langID + ASCII_REPORT_SEPARATOR);

            Debug.WriteLine("Changing language to {0}", langID);
            int code = _terminal.SendTLD(args);
            return _terminal.MethodRejectCode;
        }

        public void Open()
        {
            opening = true;

            Debug.WriteLine("Opening terminal...");
            int code = _terminal.Open();

            if (code == 0)
            {
                opening = false;
                OnError("Terminal failed to be opened", _terminal.MethodRejectCode, code);
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
            return !IsOpen() && !opening;
        }

        internal bool IsOpen()
        {
            // TODO: IsOpen() returns true even if USB has gotten USB disconnected after successful open
            return _terminal.IsOpen();
        }

        public delegate void SuccessHandler(object sender, LocalModeEventArgs args);
        public event SuccessHandler OnSuccess = delegate { };

        public delegate void ReadyHandler(object sender, TerminalReadyEventArgs args);
        public event ReadyHandler OnReady = delegate { };

        public delegate void ErrorHandler(string errorMessage, int errorCode, int errorCodeParent);
        public event ErrorHandler OnError = delegate { };

        public delegate void PrintTextHandler(object sender, PrintTextEventArgs args);
        public event PrintTextHandler OnPrintText = delegate { };

        public delegate void DisplayTextHandler(object sender, DisplayTextEventArgs args);
        public event DisplayTextHandler OnDisplayText = delegate { };

        public delegate void OpenHandler(object sender, LocalModeEventArgs args);
        public event OpenHandler OnOpen = delegate { };

        private void HandleLocalMode(object sender, LocalModeEventArgs args)
        {
            // Unknown result: will be taken care of by the error handler (HandleError)
            if (args.Result == 99) { return; }

            if (args.Result == 2)
            {
                OnError("Rejected", -1, args.Result);
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

        private void HandleDisplayText(object sender, DisplayTextEventArgs args)
        {
            OnDisplayText(sender, args);
        }

        private void HandleJsonReceived(object sender, JsonReceivedArgs args)
        {
            Debug.Write("JSON received: {0}", args.JsonString);
        }

        internal TerminalInfo GetInfo()
        {
            return new TerminalInfo(_terminal);
        }

        internal int PingHost()
        {
            SendTldArgs args = new SendTldArgs();
            args.TldType = "REQ";
            args.TldField = Encoding.ASCII.GetBytes("1012");
            int code = _terminal.SendTLD(args);
            return _terminal.MethodRejectCode;
        }
    }
}
