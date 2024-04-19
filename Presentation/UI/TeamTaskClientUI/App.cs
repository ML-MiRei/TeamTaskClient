using System.Windows;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI
{
    public class App : Application
    {
        readonly MainWindow mainWindow;


        public App(MainWindow mainWindow)
        {



            this.mainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
