
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
using TeamTaskClient.Infrastructure.Services.Interfaces;

namespace TeamTaskClient.UI.Login.Modules.ViewModels
{
    public class SigninPageVM : ViewModelBase
    {
        private static IAuthorizationService _authorizationService;


        private static SigninPageVM _instance;
        public static SigninPageVM GetInstance(IAuthorizationService authorizationService)
        {

            if (_instance == null)
                _instance = new SigninPageVM(authorizationService);
            return _instance;

        }


        private SigninPageVM(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;


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

                var user = await _authorizationService.Authorize(_email, _password);

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
                LoginWindow loginWindow = Programm.LoginWindow;
                loginWindow.frameLayout.NavigationService.Navigate(new SignUpPage(_authorizationService));
            }
        }

    }
}
