using MediatR;
using System.Windows;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Profile.View;

namespace TeamTaskClientUI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMediator mediator, IMessengerEvents messengerEvents, IMessengerCash messengerCash, 
            IProjectsCash projectsCash, INotificationCash notificationCash, ITeamsCash teamsCash)
        {
            InitializeComponent();

            DataContext = new MainWindowVM(this, mediator, messengerEvents, messengerCash, projectsCash, notificationCash, teamsCash);
            profileButton.IsChecked = true;

            frameLayuot.NavigationService.Navigate(new ProfilePage(mediator, notificationCash));

        }


        protected override void OnDeactivated(EventArgs e)
        {
            Deactivated.Visibility = Visibility.Visible;
            base.OnDeactivated(e);

        }


        protected override void OnActivated(EventArgs e)
        {
            Deactivated.Visibility = Visibility.Collapsed;
            base.OnActivated(e);
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }
}