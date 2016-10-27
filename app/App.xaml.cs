using CefSharp;
using System.Windows;

namespace Timma
{
    public static class Version
    {
        public const string Value = "1.0";
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var settings = new CefSettings();
            settings.SetOffScreenRenderingBestPerformanceArgs();

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
