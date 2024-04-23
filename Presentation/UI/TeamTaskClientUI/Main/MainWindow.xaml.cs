using MediatR;
using System.Windows;
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
        private static IMediator m;

        public MainWindow(IMediator mediator)
        {
            InitializeComponent();
            m = mediator;

            DataContext = new MainWindowVM(this, mediator);

            profileButton.IsChecked = true;

            frameLayuot.NavigationService.Navigate(new ProfilePage(mediator));

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