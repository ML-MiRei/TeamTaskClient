using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamTaskClientUI.Main;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using System.Configuration;
using TeamTaskClient.UI.Login;
using TeamTaskClient.UI.Dialogs.View;

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
