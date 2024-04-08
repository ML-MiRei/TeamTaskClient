using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.User.Commands.DeleteUser;
using TeamTaskClient.ApplicationLayer.CQRS.User.Commands.UpdateUser;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.UI.Modules.Profile.Models;


namespace TeamTaskClient.UI.Modules.Profile.ViewModels
{
    internal class ProfileVM : ViewModelBase
    {
        private static IMediator _mediator;

        public ProfileVM(IMediator mediator, IRemoveCash removeCash)
        {

            _mediator = mediator;
            var user = mediator.Send(new GetUserByIdQuery() { UserId = Convert.ToInt32(Properties.Settings.Default.userId) }).Result;

            User = new UserModel();
            User.FirstName = user.FirstName;
            User.LastName = user.LastName;
            User.PhoneNumber = user.PhoneNumber;
            User.Email = user.Email;
            User.Tag = user.Tag;
            User.SecondName = user.SecondName;

            Logout = new LogoutCommand(removeCash);
            DeleteAccount = new DeleteAccountCommand(removeCash);
            EditProfile = new EditProfileCommand(this);
        }


        public UserModel User { get; set; }


        public ICommand Logout { get; }
        public ICommand DeleteAccount { get; }
        public ICommand EditProfile { get; }






        private class EditProfileCommand(ProfileVM profileVM) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                InputDialogWindow inputDialog = new InputDialogWindow("User data", "Save", new List<string> {
                    profileVM.User.FirstName, profileVM.User.SecondName, profileVM.User.LastName, profileVM.User.PhoneNumber
                });
                if (inputDialog.ShowDialog().Value)
                {
                    var userModel = ((InputDialogVM)inputDialog.DataContext);
                    _mediator.Send(new UpdateUserCommand
                    {
                        FirstName = userModel.InputValues[0].Text,
                        SecondName = userModel.InputValues[1].Text,
                        LastName = userModel.InputValues[2].Text,
                        Phone = userModel.InputValues[3].Text,
                        UserId = Properties.Settings.Default.userId
                    });

                    profileVM.User = new UserModel()
                    {
                        FirstName = userModel.InputValues[0].Text,
                        SecondName = userModel.InputValues[1].Text,
                        LastName = userModel.InputValues[2].Text,
                        PhoneNumber = userModel.InputValues[3].Text,
                        Email = profileVM.User.Email
                    };

                    profileVM.OnPropertyChanged(nameof(User));
                }
            }
        }


        private class LogoutCommand(IRemoveCash removeCash) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                removeCash.RemoveCash();
                Properties.Settings.Default.userId = 0;
                Properties.Settings.Default.userTag = "";
                Properties.Settings.Default.Save();


                App.Current.Shutdown();
            }
        }


        private class DeleteAccountCommand(IRemoveCash removeCash) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                AlertDialogWindow alertDialog = new AlertDialogWindow("All your data will be deleted. Are you sure you want to delete your account?", "Delete", "Cancel");
                if (alertDialog.ShowDialog().Value)
                {
                    try
                    {
                        _mediator.Send(new DeleteUserCommand { UserId = Properties.Settings.Default.userId });

                        removeCash.RemoveCash();
                        Properties.Settings.Default.userId = 0;
                        Properties.Settings.Default.userTag = "";
                        Properties.Settings.Default.Save();

                        App.Current.Shutdown();
                    }
                    catch
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Account deletion error");
                        errorWindow.ShowDialog();
                    }


                }


            }
        }
    }
}
