using MediatR;
using System.Windows;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Profile.View;

namespace TeamTaskClientUI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IMediator m;
        private static IRemoveCash r;

        public MainWindow(IMediator mediator, IRemoveCash removeCash)
        {
            InitializeComponent();
            m = mediator;
            r = removeCash;

            DataContext = new MainWindowVM(this, mediator, removeCash);

            profileButton.IsChecked = true;

            frameLayuot.NavigationService.Navigate(new ProfilePage(mediator, removeCash));

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
    }
}