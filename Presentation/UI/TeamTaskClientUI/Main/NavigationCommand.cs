using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TeamTaskClient.UI;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Main
{
    internal class NavigationCommand : CommandBase
    {

        private Page _page;

        public NavigationCommand(Page page)
        {
            _page = page;
        }


        public override void Execute(object parameter)
        {
            MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainWindow.frameLayuot.NavigationService.Navigate(_page);
        }
    }
}
