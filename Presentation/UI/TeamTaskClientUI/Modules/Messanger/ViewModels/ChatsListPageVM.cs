using MediatR;

using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.AddUserInGroupChat;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChat;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreatePrivateChat;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.LeaveChat;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.UpdateGroupChat;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.UseCases.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.UI.Modules.Messanger.View;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class ChatsListPageVM : ViewModelBase
    {

        private static IMediator _mediator;
        private static IMessengerEvents _messengerEvents;

        public ChatsListPageVM(IMediator mediator, IMessengerEvents messengerEvents)
        {
            _mediator = mediator;
            _messengerEvents = messengerEvents;
            MessengerStorage.Chats = new ObservableCollection<ChatModel>(mediator.Send(new GetChatsQuery() { UserId = Properties.Settings.Default.userId }).Result.ToList());

        }


        public ObservableCollection<ChatModel> Chats
        {
            get { return MessengerStorage.Chats; }
        }



        public async void SettingsChatOpen(ChatModel chatModel)
        {

            if (chatModel.Type == (int)ChatTypeEnum.PRIVATE)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Delete" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");
                        if (alertDialogWindow.ShowDialog().Value)
                        {

                            await _mediator.Send(new LeaveChatCommand { ChatId = chatModel.ChatId, UserId = Properties.Settings.Default.userId });
                        }
                    }
                    catch (Exception)
                    {
                        ErrorWindow.Show("Error leave chat");

                    }
                }
            }
            else
            {

                var listActions = new List<string> { "Leave", "Add user", "Users" };
                if (chatModel.UserRole == (int)UserRoleEnum.LEAD)
                {
                    listActions.Add("Change name");

                    if (chatModel.Users.Count > 1)
                    {
                        listActions.Remove("Leave");
                    }
                }

                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", listActions);

                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    switch (SelectActionsDialogVM.SelectedAction)
                    {
                        case "Add user":

                            InputDialogWindow updatePropertiesDialogWindow = new InputDialogWindow("User tag", "Add", new List<string> { "" });
                            if (updatePropertiesDialogWindow.ShowDialog().Value)
                            {
                                var userTag = updatePropertiesDialogWindow.GetInputValue()[0];
                                if (MessengerStorage.Chats.First(c => c.ChatId == chatModel.ChatId).Users.Any(u => u.UserTag == userTag))
                                {
                                    ErrorWindow.Show("This user is already\n a member of the chat");
                                }
                                else
                                {
                                    await _mediator.Send(new AddUserInGroupChatCommand { ChatId = chatModel.ChatId, UserTag = userTag });
                                }
                            }


                            break;
                        case "Leave":
                            try
                            {
                                AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");
                                if (alertDialogWindow.ShowDialog().Value)
                                {
                                    await _mediator.Send(new LeaveChatCommand { ChatId = chatModel.ChatId, UserId = Properties.Settings.Default.userId });
                                }
                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error leave chat");
                            }
                            break;

                        case "Change name":

                            InputDialogWindow changeNameDialogWindow = new InputDialogWindow("Chat name", "Change", new List<string> { chatModel.ChatName });
                            if (changeNameDialogWindow.ShowDialog().Value)
                            {
                                var name = changeNameDialogWindow.GetInputValue()[0];

                                await _mediator.Send(new UpdateGroupChatCommand { ChatId = chatModel.ChatId, ChatName = name, AdminTag = Properties.Settings.Default.userTag });

                            }
                            break;

                        case "Change admin":

                            InputDialogWindow changeAdminDialogWindow = new InputDialogWindow("Admin tag", "Change", new List<string> { "" });
                            if (changeAdminDialogWindow.ShowDialog().Value)
                            {
                                var tag = changeAdminDialogWindow.GetInputValue()[0];

                                await _mediator.Send(new UpdateGroupChatCommand { ChatId = chatModel.ChatId, AdminTag = tag });
                            }
                            break;
                        case "Users":

                            UsersChatWindow usersChatWindow = new UsersChatWindow(chatModel.ChatId, _mediator, _messengerEvents);
                            usersChatWindow.ShowDialog();

                            break;

                    }
                }

            }


        }




        public ICommand AddButton { get; set; } = new AddChatCommand();
        private class AddChatCommand : CommandBase
        {
            public async override void Execute(object? parameter)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Create private chat", "Create group chat" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    switch (selectActionsDialogWindow.GetSelectedAction())
                    {
                        case "Create private chat":

                            InputDialogWindow window = new InputDialogWindow("Enter user's tag", "Find user", new List<string> { "Tag" });
                            if (window.ShowDialog().Value)
                            {
                                string userTag = window.GetInputValue()[0];

                                if (MessengerStorage.Chats.Any(c => c.Type == (int)ChatTypeEnum.PRIVATE && c.Users.Any(u => u.UserTag == userTag)))
                                {

                                    ErrorWindow.Show("There is already such a chat");
                                    return;
                                }

                                try
                                {
                                    var user = _mediator.Send(new GetUserByTagQuery { UserTag = userTag }).Result;
                                    AlertDialogWindow alertDialogWindow = new AlertDialogWindow($"Founded user: {user.FirstName} {user.SecondName} {user.LastName}", "Complete", "Cancel");
                                    if (alertDialogWindow.ShowDialog().Value)
                                    {
                                        try
                                        {
                                            await _mediator.Send(new CreatePrivateChatCommand { SecondUserTag = userTag, UserId = Properties.Settings.Default.userId });
                                        }
                                        catch
                                        {
                                            ErrorWindow.Show("Error creating private chat");
                                        }
                                    }

                                }
                                catch
                                {
                                    ErrorWindow.Show("User not found");

                                }

                            }


                            break;
                        case "Create group chat":


                            CreateSubjectDialogWindow windowGroup = new CreateSubjectDialogWindow("Enter group name", new List<string> { "Name" });
                            if (windowGroup.ShowDialog().Value)
                            {
                                string nameGroup = windowGroup.GetCreatingProperties()[0];

                                try
                                {
                                    await _mediator.Send(new CreateGroupChatCommand { Name = nameGroup, UserId = Properties.Settings.Default.userId });

                                }
                                catch
                                {
                                    ErrorWindow.Show("Error creating group chat");
                                }


                            }
                            break;
                    }
                }
            }
        }
    }
}
