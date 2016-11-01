using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using CefSharp;
using CefSharp.Wpf;
using BBS.BAXI;
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
        private bool browserReloading = false;

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
            terminalCtrl = new TerminalController(terminal);
            terminalCtrl.Initialize();

            browser = new ChromiumWebBrowser()
            {
                Address = Address
            };

            TimmaBrowser.Children.Add(browser);

            browserApi = new BrowserController(browser, terminalCtrl);

            browser.FrameLoadEnd += HandleFrameLoaded;

            CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, Reload));
        }

        private void HandleFrameLoaded(object sender, FrameLoadEndEventArgs e)
        {
            if (!e.Frame.IsMain) { return; }
            if (browserReloading)
            {
                browserReloading = false;
            }

            if (terminalCtrl.CanOpen())
            {
                terminalCtrl.Open();
            }
        }

        private void Reload(object sender, ExecutedRoutedEventArgs e)
        {
            browserReloading = true;
            Debug.WriteLine("RELOAD");

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
