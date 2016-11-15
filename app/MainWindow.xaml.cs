using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using CefSharp;
using CefSharp.Wpf;
using BBS.BAXI;
using Timma.Browser;
using Timma.Terminal;

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
                #if (PRODUCTION && !DEBUG && !LOCAL)
                return "https://timma.fi/admin";
                #elif (PRODUCTION && DEBUG && !LOCAL)
                return "http://testi.timma.fi/admin";
                #else
                return "http://admin.timma.dev";
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

            browserApi = new BrowserController(browser, terminalCtrl);

            browser.FrameLoadEnd += HandleFrameLoaded;

            TimmaBrowser.Children.Add(browser);

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
            System.Diagnostics.Debug.WriteLine("RELOAD");
            browserReloading = true;
            browser.Reload(true);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you wish to exit the application?", "Timma", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

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
