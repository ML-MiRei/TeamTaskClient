using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Login.Modules.View;

namespace TeamTaskClient.UI.Login.Modules.ViewModels
{
    public class SignupPageVM : ViewModelBase
    {
        private static IAuthorizationService _authorizationService;


        private static SignupPageVM _instance;
        public static SignupPageVM GetInstance(IAuthorizationService authorizationService)
        {

            if (_instance == null)
                _instance = new SignupPageVM(authorizationService);
            return _instance;

        }


        private SignupPageVM(IAuthorizationService authorizationService)
        {

            _authorizationService = authorizationService;
            SignupButton = new SignupCommand(); ;
            ToSignin = new ToSigninCommand();

            Email = "Enter E-mail";
            Password = "Enter password";
            RepeatPassword = "Repeat password";


            ToSignup = new ToSignupCommand();


            CompleteButton = new CompleteRegisterCommand();

            FirstName = "Enter first name";
            SecondName = "Enter second name";
            LastName = "Enter last name";
            Phone = "Enter phone number";


        }

        private static string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private static string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }


        private static string _secondName;
        public string SecondName
        {
            get
            {
                return _secondName;
            }
            set
            {
                _secondName = value;
                OnPropertyChanged("SecondName");
            }
        }



        private static string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public ICommand CompleteButton { get; }
        public ICommand ToSignup { get; }


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
            set { _password = value; OnPropertyChanged("Password"); }
        }



        private static string _repeatPassword;
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { _repeatPassword = value; OnPropertyChanged("RepeatPassword"); }
        }


        public ICommand ToSignin { get; }
        public ICommand SignupButton { get; }




        private class SignupCommand : CommandBase
        {
            public override void Execute(object parameter)
            {

                LoginWindow loginWindow = Programm.LoginWindow;
                loginWindow.frameLayout.NavigationService.Navigate(new SignUpTwoPage(_authorizationService));

            }
        }
        private class ToSigninCommand : CommandBase
        {
            public override void Execute(object parameter)
            {
                LoginWindow loginWindow = Programm.LoginWindow;
                loginWindow.frameLayout.NavigationService.Navigate(new SignInPage(_authorizationService));
            }
        }


        private class ToSignupCommand : CommandBase
        {
            public override void Execute(object parameter)
            {
                LoginWindow loginWindow = Programm.LoginWindow; ;
                loginWindow.frameLayout.NavigationService.Navigate(new SignUpPage(_authorizationService));
            }
        }


        private class CompleteRegisterCommand : CommandBase
        {
            public override void Execute(object parameter)
            {

                _authorizationService.Register(new Domain.Entities.UserEntity
                {
                    Email = _email,
                    Password = _password,
                    FirstName = _firstName,
                    LastName = _lastName,
                    PhoneNumber = _phone,
                    SecondName = _secondName != "Enter second name" ? _secondName : ""

                });
                Programm.LoginWindow.DialogResult = true;
                return;


            }
        }
    }
}
