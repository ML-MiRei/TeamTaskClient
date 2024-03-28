
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Modules.Messanger.View;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClient.UI.Modules.Projects.View;

namespace TeamTaskClient.UI.Main
{
    internal class MainWindowVM : ViewModelBase
    {

        public MainWindowVM()
        {

            ProfileButton = new NavigationCommand(new ProfilePage());
            ProjectsButton = new NavigationCommand(new ProjectsPage());
            MessangerButton = new NavigationCommand(new MessangerPage());
            //CalendarButton = new NavigationCommand(new CalendarPage());
            //TasksButton = new NavigationCommand(new TaskPage());

            //MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            //mainWindow.frameLayuot.NavigationService.Navigate(new ProfilePage());
            //mainWindow.profileButton.IsChecked = true;

        }


        public ICommand ProfileButton { get; }
        public ICommand ProjectsButton { get; }
        public ICommand MessangerButton { get; }
        public ICommand TasksButton { get; }
        public ICommand CalendarButton { get; }

    }
}
