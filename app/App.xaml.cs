using CefSharp;
using System.Windows;
using System;
using System.Collections.Generic;
using Microsoft.Shell;

namespace NetsExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        public static string Version {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }

        private const string Unique = "DCD935F5-6766-4AF6-8B8E-DBEC01285385";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // Bring window to foreground
            if (this.MainWindow.WindowState == WindowState.Minimized)
            {
                this.MainWindow.WindowState = WindowState.Normal;
            }

            this.MainWindow.Activate();

            return true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var settings = new CefSettings();
            settings.SetOffScreenRenderingBestPerformanceArgs();

            #if (!PRODUCTION)
            settings.IgnoreCertificateErrors = true; // allow self-signed certs
            #endif

            CefSharpSettings.WcfEnabled = false;
            
            if (Cef.Initialize(settings))
            {
                base.OnStartup(e);
            }
            else
            {
                Current.Shutdown();
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            if (Cef.IsInitialized)
            {
                Cef.Shutdown();
            }
        }
    }

}
