using System.Windows;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Login.Modules.View;


namespace TeamTaskClient.UI.Login
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(IAuthorizationService authorizationService)
        {
            InitializeComponent();
            frameLayout.NavigationService.Navigate(new SignInPage(authorizationService));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (Properties.Settings.Default.userId == 0)
                App.Current.Shutdown();

        }
    }
}
