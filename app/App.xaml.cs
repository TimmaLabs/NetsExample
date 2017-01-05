using CefSharp;
using System.Windows;

namespace Timma
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Version {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var settings = new CefSettings();
            settings.SetOffScreenRenderingBestPerformanceArgs();

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
