using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using CefSharp;
using CefSharp.Wpf;
using BBS.BAXI;
using System.Threading;
using Timma.Browser;
using System.Diagnostics;

namespace Timma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChromiumWebBrowser browser;
        private BrowserController browserApi;
        private TerminalController terminalCtrl;
        private ErrorBox box;
        private SynchronizationContext ctx;
        private bool browserReloading;

        private string Address {
            get
            {
                #if DEBUG
                return "http://admin.timma.dev";
                #else
                return "http://testi.timma.fi/admin";
                #endif
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            BaxiCtrl terminal = new BaxiCtrl();
            browser = new ChromiumWebBrowser()
            {
                Address = Address
            };

            terminalCtrl = new TerminalController(terminal);
            browserApi = new BrowserController(browser, terminalCtrl);
            box = new ErrorBox(this);
            ctx = SynchronizationContext.Current;

            TimmaBrowser.Children.Add(browser);

            terminalCtrl.OnError += HandleTerminalError;
            terminalCtrl.OnOpen += HandleTerminalOpen;
            terminalCtrl.OnSuccess += HandleTerminalSuccess;
            browser.FrameLoadEnd += HandleFrameLoaded;

            terminalCtrl.Initialize();

            CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, Reload));
        }

        private void HandleTerminalSuccess(object sender, LocalModeEventArgs args)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(args);
            Debug.WriteLine(json);
            Debug.WriteLine("OnSuccess result: {0}", args.Result);
        }

        private void HandleTerminalOpen(object sender, LocalModeEventArgs args)
        {
            Debug.WriteLine("IS OPEN");
            ctx.Post(HideErrorBox, null);
        }

        private void HideErrorBox(object state)
        {
            box.Hide();
        }

        private void HandleFrameLoaded(object sender, FrameLoadEndEventArgs e)
        {
            if (!e.Frame.IsMain) { return; }
            if (browserReloading)
            {
                browserReloading = false;
            }
        }

        private void HandleTerminalError(string errorMessage, int errorCode, int errorCodeParent)
        {
            Dictionary<string, string> errorData = new Dictionary<string, string>()
            {
                { "code", errorCode.ToString() },
                { "codeParent", errorCodeParent.ToString() },
                { "message", errorMessage }
            };
            ctx.Post(DisplayTerminalError, errorData);
        }

        private void DisplayTerminalError(object state)
        {
            var error = state as Dictionary<string, string>;

            if (error["code"].Equals("2011"))
            {
                terminalCtrl.Close();
                box.SetMessage("Payment terminal not connected. Please re-connect the terminal USB cable and press Ctrl+R or F5, or restart the application.");
                box.Show();
            }
        }

        private void Reload(object sender, ExecutedRoutedEventArgs e)
        {
            browserReloading = true;
            Debug.WriteLine("RELOAD");
            
            if (terminalCtrl.CanOpen())
            {
                terminalCtrl.Open();
            }

            browser.Reload(true);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result =
                  MessageBox.Show(
                    "Are you sure you want to exit the application?",
                    "Timma",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                terminalCtrl.Close();
            }
        }
    }
}
