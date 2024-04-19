using System.Windows.Controls;
using TeamTaskClient.UI.Common.Base;
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
