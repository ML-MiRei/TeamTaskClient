using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.ApplicationLayer.UseCases.Notification.Commands.DeleteNotification;
using TeamTaskClient.ApplicationLayer.UseCases.Notification.Queries.GetNotifications;
using TeamTaskClient.ApplicationLayer.UseCases.User.Commands.DeleteUser;
using TeamTaskClient.ApplicationLayer.UseCases.User.Commands.UpdateUser;
using TeamTaskClient.ApplicationLayer.UseCases.User.Queries.GetUserById;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;


namespace TeamTaskClient.UI.Modules.Profile.ViewModels
{
    internal class ProfileVM : ViewModelBase
    {
        private static IMediator _mediator;
        private static INotificationCash _notificationCash;


        public static ProfileVM profile;

        public ProfileVM(IMediator mediator, INotificationCash notificationCash)
        {

            _mediator = mediator;
            _notificationCash = notificationCash;

            User = mediator.Send(new GetUserByIdQuery() { UserId = Convert.ToInt32(Properties.Settings.Default.userId) }).Result;

            profile = this;
            notificationCash.Notifications =
                new ObservableCollection<NotificationModel>(mediator.Send(new GetNotificationsQuery { UserId = Properties.Settings.Default.userId }).Result);

            Logout = new LogoutCommand();
            DeleteAccount = new DeleteAccountCommand();
            EditProfile = new EditProfileCommand(this);
        }

        public string Lit => User.Lit;
        public int ColorNumber => User.ColorNumber;

        public UserModel User { get; set; }


        public ICommand Logout { get; }
        public ICommand DeleteAccount { get; }
        public ICommand EditProfile { get; }


        public ObservableCollection<NotificationModel> Notifications
        {
            get
            {
                return _notificationCash.Notifications;
            }
        }


        public void DeleteNotification(NotificationModel notificationModel)
        {

            try
            {

                AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Delete a message?", "Yes", "Cancel");

                if (alertDialogWindow.ShowDialog().Value)
                {
                    _mediator.Send(new DeleteNotificationCommand { NotificationId = notificationModel.NotificationId });

                    _notificationCash.Notifications.Remove(notificationModel);
                    OnPropertyChanged(nameof(Notifications));
                }

            }
            catch (Exception)
            {
                ErrorWindow.Show("Error notification delete");

            }


        }


        private class EditProfileCommand(ProfileVM profileVM) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                InputDialogWindow inputDialog = new InputDialogWindow("User data", "Save", new List<string> {
                    profileVM.User.FirstName, profileVM.User.SecondName, profileVM.User.LastName, profileVM.User.PhoneNumber
                });
                if (inputDialog.ShowDialog().Value)
                {
                    var inputData = ((InputDialogVM)inputDialog.DataContext);


                    _mediator.Send(new UpdateUserCommand
                    {
                        FirstName = String.IsNullOrEmpty(inputData.InputValues[0].Text) ? null : inputData.InputValues[0].Text,
                        SecondName = inputData.InputValues[1].Text,
                        LastName = String.IsNullOrEmpty(inputData.InputValues[2].Text) ? null : inputData.InputValues[2].Text,
                        Phone = String.IsNullOrEmpty(inputData.InputValues[3].Text) ? null : inputData.InputValues[3].Text,
                        UserId = Properties.Settings.Default.userId
                    });


                    profileVM.User.FirstName = String.IsNullOrEmpty(inputData.InputValues[0].Text) ? profileVM.User.FirstName : inputData.InputValues[0].Text;
                    profileVM.User.SecondName = inputData.InputValues[1].Text;
                    profileVM.User.LastName = String.IsNullOrEmpty(inputData.InputValues[2].Text) ? profileVM.User.LastName : inputData.InputValues[2].Text;
                    profileVM.User.PhoneNumber = String.IsNullOrEmpty(inputData.InputValues[3].Text) ? profileVM.User.PhoneNumber : inputData.InputValues[3].Text;


                    profileVM.OnPropertyChanged(nameof(User));
                }
            }
        }


        private class LogoutCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                Properties.Settings.Default.userId = 0;
                Properties.Settings.Default.userTag = "";
                Properties.Settings.Default.Save();


                App.Current.Shutdown();
            }
        }


        private class DeleteAccountCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                AlertDialogWindow alertDialog = new AlertDialogWindow("All your data will be deleted. \nAre you sure you want to delete your account?", "Delete", "Cancel");
                if (alertDialog.ShowDialog().Value)
                {
                    try
                    {
                        _mediator.Send(new DeleteUserCommand { UserId = Properties.Settings.Default.userId });

                        Properties.Settings.Default.userId = 0;
                        Properties.Settings.Default.userTag = "";
                        Properties.Settings.Default.Save();

                        App.Current.Shutdown();
                    }
                    catch
                    {
                        ErrorWindow.Show("Account deletion error");
                    }


                }


            }
        }
    }
}
