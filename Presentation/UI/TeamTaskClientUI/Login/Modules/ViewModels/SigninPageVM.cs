
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Diagnostics.Metrics;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.UI.Login.Modules.View;
using TeamTaskClient.Infrastructure.Services.Implementation;
using MediatR;
using System.Configuration;

namespace TeamTaskClient.UI.Login.Modules.ViewModels
{
    public class SigninPageVM : ViewModelBase
    {

        private static SigninPageVM _instance;
        public static SigninPageVM Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SigninPageVM();
                return _instance;
            }
        }


        private SigninPageVM()
        {
            SigninButton = new SigninCommand();
            ToSignup = new ToSignupCommand();
            ForgotPassword = null;
            Email = "Enter E-mail";
            Password = "Enter password";
        }



        private static string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }


        private static string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


        public ICommand ForgotPassword { get; }
        public ICommand SigninButton { get; } = new SigninCommand();
        public ICommand ToSignup { get; } 


        private class SigninCommand : CommandBase
        {
            public async override void Execute(object parameter)
            {
                
                AuthorizationService authorizationService = new AuthorizationService();
                var user = await authorizationService.Authorize(_email, _password);

                Properties.Settings.Default.userId = user.ID;
                Properties.Settings.Default.userTag = user.Tag;
                Properties.Settings.Default.Save();               

                Programm.LoginWindow.DialogResult = true;

            }
        }

        private class ToSignupCommand : CommandBase
        {
            public override void Execute(object parameter)
            {
                LoginWindow loginWindow = App.Current.Windows.OfType<LoginWindow>().First();
                loginWindow.frameLayout.NavigationService.Navigate(new SignUpPage());
            }
        }

    }
}
