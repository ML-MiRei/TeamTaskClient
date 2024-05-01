
using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
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
        private static IProjectsCash _projectsCash;


        public MainWindowVM(MainWindow mainWindow, IMediator mediator, 
            IMessengerEvents messengerEvents, IMessengerCash messengerCash, IProjectsCash projectsCash,  INotificationCash notificationCash, ITeamsCash teamsCash)
        {
            _mainWindow = mainWindow;
            _mediator = mediator;
            _projectsCash = projectsCash;

            ProfileButton = new NavigationCommand(mainWindow, new ProfilePage(mediator, notificationCash));
            ProjectsButton = new NavigationCommand(mainWindow, new ObserveProjectsPage(mediator, projectsCash));
            MessangerButton = new NavigationCommand(mainWindow, new MessangerPage(mediator, messengerEvents, messengerCash));
            TeamsButton = new NavigationCommand(mainWindow, new TeamPage(mediator, teamsCash));
        }


        public static void ToProjectTaskButton()
        {
            _mainWindow.frameLayuot.NavigationService.Navigate(new ProjectPage(_mediator, _projectsCash));

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
