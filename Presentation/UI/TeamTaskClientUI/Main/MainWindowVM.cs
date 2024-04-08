
using MediatR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Login;
using TeamTaskClient.UI.Modules.Messanger.View;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClient.UI.Modules.Projects.View;
using TeamTaskClient.UI.Modules.Teams.View;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Main
{
    internal class MainWindowVM : ViewModelBase
    {

        public MainWindowVM(MainWindow mainWindow, IMediator mediator, IRemoveCash removeCash)
        {

            ProfileButton = new NavigationCommand(mainWindow, new ProfilePage(mediator, removeCash));
            ProjectsButton = new NavigationCommand(mainWindow, new ProjectsPage());
            MessangerButton = new NavigationCommand(mainWindow, new MessangerPage(mediator));
            ToProjectTaskButton = new NavigationCommand(mainWindow, new ProjectTasksPage(mediator));
            TeamsButton = new NavigationCommand(mainWindow, new TeamPage(mediator));




            //TasksButton = new NavigationCommand(new TaskPage());

            //MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            //mainWindow.frameLayuot.NavigationService.Navigate(new ProfilePage());
            //mainWindow.profileButton.IsChecked = true;

        }


        public static ICommand ToProjectTaskButton { get; set; }


        public ICommand ProfileButton { get; }
        public ICommand ProjectsButton { get; }
        public ICommand MessangerButton { get; }
        public ICommand TasksButton { get; }
        public ICommand TeamsButton { get; }

    }
}
