using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TeamTaskClient.UI;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Main
{
    internal class NavigationCommand : CommandBase
    {

        private Page _page;
        private MainWindow _mainWindow;

        public NavigationCommand(MainWindow mainWindow, Page page)
        {
            _page = page;
            _mainWindow = mainWindow;
        }


        public override void Execute(object parameter)
        {
           
            //MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            _mainWindow.frameLayuot.NavigationService.Navigate(_page);

        }
    }
}
