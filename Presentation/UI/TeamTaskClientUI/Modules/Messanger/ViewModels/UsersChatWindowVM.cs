using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.DeleteUserFromGroupChat;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.UpdateGroupChat;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    public class UsersChatWindowVM : ViewModelBase
    {
        IMediator _mediator;
        IMessengerCash _messengerCash;
        private int _chatId;

        public UsersChatWindowVM(int chatId, IMediator mediator, IMessengerEvents messengerEvents, IMessengerCash messengerCash)
        {
            _mediator = mediator;
            _messengerCash = messengerCash;
            _chatId = chatId;
            Users = new ObservableCollection<UserModel>(_messengerCash.Chats.First(c => c.ChatId == chatId).Users);

            messengerEvents.UserFromChatDeleted += OnDeleteUserFromChat;
            messengerEvents.AddNewUserChat += OnAddNewUserChat;
        }

        private void OnAddNewUserChat(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (_chatId == (int)sender)
                    Users.Add(e);
            });
        }

        private void OnDeleteUserFromChat(object? sender, string e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (_chatId == (int)sender)
                    Users.Remove(Users.FirstOrDefault(u => u.UserTag == e));
            });
        }

        public async void ClickUser(string userTag)
        {
            SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Kick user", "Make admin" });
            if (selectActionsDialogWindow.ShowDialog().Value)
            {
                var action = selectActionsDialogWindow.GetSelectedAction();
                switch (action)
                {
                    case "Kick user":

                        AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                        if (alertDialogWindow.ShowDialog().Value)
                        {
                            try
                            {
                                await _mediator.Send(new DeleteUserFromGroupChatCommand { ChatId = _chatId, UserTag = userTag });
                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error user deleted");
                            }
                        }

                        break;

                    case "Make admin":
                        AlertDialogWindow alertDialogWindow1 = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                        if (alertDialogWindow1.ShowDialog().Value)
                        {
                            try
                            {
                                await _mediator.Send(new UpdateGroupChatCommand { AdminTag = userTag, ChatId = _chatId });

                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error user madke admin");
                            }
                        }
                        break;
                }
            }
        }

        public ObservableCollection<UserModel> Users { get; set; }



    }
}
