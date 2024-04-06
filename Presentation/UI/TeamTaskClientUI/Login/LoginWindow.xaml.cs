
using System;
using System.ComponentModel;
using System.Windows;
using TeamTaskClient.UI.Login.Modules;
using TeamTaskClient.UI.Login.Modules.View;


namespace TeamTaskClient.UI.Login
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            frameLayout.NavigationService.Navigate(new SignInPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
