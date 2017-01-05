using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using CefSharp;
using CefSharp.Wpf;
using BBS.BAXI;
using Timma.Browser;
using Timma.Terminal;
using System;

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
        private double maxZoomLevel = 2.0;
        private double minZoomLevel = -1.0;

        private string Address {
            get
            {
                #if (PRODUCTION && !LOCALHOST)
                return "https://timma.fi/admin";
                #else
                return "http://customer.timma.dev";
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
            browser.ZoomLevelIncrement = 0.25;

            TimmaBrowser.Children.Add(browser);

            CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, Reload));
            CommandBindings.Add(new CommandBinding(NavigationCommands.IncreaseZoom, ZoomIn));
            CommandBindings.Add(new CommandBinding(NavigationCommands.DecreaseZoom, ZoomOut));
            CommandBindings.Add(new CommandBinding(NavigationCommands.Zoom, ZoomReset));
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

        private void ZoomIn(object sender, ExecutedRoutedEventArgs e)
        {
            if (browser.ZoomLevel < maxZoomLevel)
            {
                browser.ZoomInCommand.Execute(null);
            }
        }

        private void ZoomOut(object sender, ExecutedRoutedEventArgs e)
        {
            if (browser.ZoomLevel > minZoomLevel)
            {
                browser.ZoomOutCommand.Execute(null);
            }
        }

        private void ZoomReset(object sender, ExecutedRoutedEventArgs e)
        {
            browser.ZoomResetCommand.Execute(null);
        }

    }
}
