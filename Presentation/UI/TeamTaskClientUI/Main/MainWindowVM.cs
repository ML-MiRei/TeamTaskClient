
using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Modules.Messanger.View;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClient.UI.Modules.Projects.View;
using TeamTaskClient.UI.Modules.Teams.View;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Main
{
    internal class MainWindowVM : ViewModelBase
    {
        private static MainWindow _mainWindow;
        private static IMediator _mediator;


        public MainWindowVM(MainWindow mainWindow, IMediator mediator, IRemoveCash removeCash)
        {
            _mainWindow = mainWindow;
            _mediator = mediator;

            ProfileButton = new NavigationCommand(mainWindow, new ProfilePage(mediator, removeCash));
            ProjectsButton = new NavigationCommand(mainWindow, new ObserveProjectsPage(mediator));
            MessangerButton = new NavigationCommand(mainWindow, new MessangerPage(mediator));
            TeamsButton = new NavigationCommand(mainWindow, new TeamPage(mediator));




            //TasksButton = new NavigationCommand(new TaskPage());

        }


        public static void ToProjectTaskButton()
        {
            _mainWindow.frameLayuot.NavigationService.Navigate(new ProjectPage(_mediator));

        }
        public static void ToProjects()
        {
            _projectsBuutton.Execute(null);
        }


        public ICommand ProfileButton { get; }

        private static ICommand _projectsBuutton;
        public ICommand ProjectsButton { get => _projectsBuutton; set => _projectsBuutton = value; }
        public ICommand MessangerButton { get; }
        public ICommand TasksButton { get; }
        public ICommand TeamsButton { get; }

    }
}
