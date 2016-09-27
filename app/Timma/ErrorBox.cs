using System;

namespace Timma
{
    class ErrorBox
    {
        private MainWindow mainWindow;

        public ErrorBox(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void Hide()
        {
            mainWindow.TimmaErrorBoxWrapper.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void Show()
        {
            mainWindow.TimmaErrorBoxWrapper.Visibility = System.Windows.Visibility.Visible;
        }

        public void SetMessage(String msg)
        {
            mainWindow.TimmaErrorBox.Text = msg;
        }
    }
}
