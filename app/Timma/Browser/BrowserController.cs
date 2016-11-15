using System;
using BBS.BAXI;
using CefSharp;
using CefSharp.Wpf;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using Timma.Terminal;

namespace Timma.Browser
{
    internal class BrowserController
    {
        private readonly string NAMESPACE = "__TimmaECR";
        private TerminalController terminalCtrl;
        private ChromiumWebBrowser browser;
        private SynchronizationContext ctx;
        private bool _browserLoaded = false;

        private readonly string JS_FUNC_ONLOADED = "onLoaded";
        private readonly string JS_FUNC_ONSUCCESS = "onSuccess";
        private readonly string JS_FUNC_ONREADY = "onReady";
        private readonly string JS_FUNC_ONERROR = "onError";
        private readonly string JS_FUNC_ONPRINT = "onPrint";
        private readonly string JS_FUNC_ONDISPLAY = "onDisplay";
        private readonly string JS_FUNC_LOG = "log";

        public BrowserController(ChromiumWebBrowser browser, TerminalController terminalCtrl)
        {
            this.browser = browser;
            this.terminalCtrl = terminalCtrl;
            this.browser.RegisterJsObject(NAMESPACE, new BrowserAPI(terminalCtrl));
            this.ctx = SynchronizationContext.Current;

            terminalCtrl.OnSuccess += HandleTerminalSuccess;
            terminalCtrl.OnError += HandleTerminalError;
            terminalCtrl.OnReady += HandleTerminalReady;
            terminalCtrl.OnPrintText += HandleTerminalPrint;
            terminalCtrl.OnDisplayText += HandleTerminalDisplay;
            browser.FrameLoadEnd += HandleFrameLoaded;
        }

        private void HandleTerminalPrint(object sender, PrintTextEventArgs args)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "method", JS_FUNC_ONPRINT },
                { "payload", JsonConvert.SerializeObject(args) }
            };
            ctx.Post(CallJSCallback, data);
        }

        private void HandleTerminalDisplay(object sender, DisplayTextEventArgs args)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "method", JS_FUNC_ONDISPLAY },
                { "payload", JsonConvert.SerializeObject(args) }
            };
            ctx.Post(CallJSCallback, data);
        }

        private void HandleTerminalSuccess(object sender, LocalModeEventArgs args)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "method", JS_FUNC_ONSUCCESS },
                { "payload", JsonConvert.SerializeObject(args) }
            };
            ctx.Post(CallJSCallback, data);
        }

        private void HandleTerminalReady(object sender, TerminalReadyEventArgs args)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "method", JS_FUNC_ONREADY },
                { "payload", string.Empty }
            };
            ctx.Post(CallJSCallback, data);
        }

        private void HandleTerminalError(string errorMessage, int errorCode, int errorCodeParent)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "method", JS_FUNC_ONERROR },
                { "payload", string.Format(@"{{ ""message"": ""{0}"", ""code"": {1}, ""codeParent"": {2} }}", errorMessage, errorCode, errorCodeParent) }
            };
            ctx.Post(CallJSCallback, data);
        }


        private void CallJSCallback(object state)
        {
            var data = state as Dictionary<string, string>;
            CallJSFunction(data["method"], data["payload"]);
        }

        private void HandleFrameLoaded(object sender, FrameLoadEndEventArgs e)
        {
            AddJSFunction(JS_FUNC_ONLOADED, JSLogFunction(JS_FUNC_ONLOADED, "magenta"));
            AddJSFunction(JS_FUNC_ONSUCCESS, JSLogFunction(JS_FUNC_ONSUCCESS, "green"));
            AddJSFunction(JS_FUNC_ONREADY, JSLogFunction(JS_FUNC_ONREADY, "blue"));
            AddJSFunction(JS_FUNC_ONERROR, JSLogFunction(JS_FUNC_ONERROR, "red"));
            AddJSFunction(JS_FUNC_ONPRINT, JSLogFunction(JS_FUNC_ONPRINT, "gray"));
            AddJSFunction(JS_FUNC_ONDISPLAY, JSLogFunction(JS_FUNC_ONDISPLAY, "orange"));
            AddJSFunction(JS_FUNC_LOG, JSLogFunction());

            ShowDevTools();
            BrowserLoaded();
        }

        [ConditionalAttribute("DEBUG")]
        private void ShowDevTools()
        {
            browser.ShowDevTools();
        }

        private string JSLogFunction(string ns = "LOG", string color = "gray")
        {
            return string.Format(@"
            function(payload = '', ns = '{0}', color = '{1}') {{
                const timestamp = window.moment ? moment().format('HH:mm:ss:SSS') : (new Date()).toLocaleTimeString()
                console.log(`%c ${{timestamp}} TimmaECR:${{ns}}`, `color: ${{color}}`, payload ? '>' : '', payload)
            }}", ns, color);
        }

        private void BrowserLoaded()
        {
            _browserLoaded = true;
            CallJSFunction(JS_FUNC_ONLOADED);
        }

        private void CallJSFunction (string func, params object[] args)
        {
            if (!_browserLoaded) { return; }

            func = NAMESPACE + "." + func;
            var arguments = string.Join(",", args);
            var statement = string.Format("typeof {0} === 'function' && {0}({1})", func, arguments);
            browser.ExecuteScriptAsync(statement);
        }

        private void AddJSFunction(string name, string body, bool replace = false)
        {
            name = string.Format("{0}.{1}", NAMESPACE, name);
            if (replace)
            {
                var statement = string.Format("{0} = {1}", name, body);
                browser.ExecuteScriptAsync(statement);
            }
            else
            {
                var statement = string.Format("{0} = {0} || {1}", name, body);
                browser.ExecuteScriptAsync(statement);
            }
        }

    }
}
