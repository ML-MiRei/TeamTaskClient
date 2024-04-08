using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.AddUserInGroupChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreateGroupChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreatePrivateChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.LeaveChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.UpdateGroupChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.DeleteMessage;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.UpdateMessage;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.UI.Modules.Messanger.View;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class ChatsListPageVM : MessengerVM
    {

        private static IMediator _mediator;

        public ChatsListPageVM(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }


        public ICommand AddButton { get; set; } = new AddChatCommand();


        public void SettingsChatOpen(ChatModel chatModel)
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
                            _mediator.Send(new LeaveChatCommand { ChatId = chatModel.ChatId, UserId = Properties.Settings.Default.userId });
                        }
                    }
                    catch (Exception)
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Error leave chat");
                        errorWindow.ShowDialog();
                    }
                }
            }
            else
            {

                var listActions = new List<string> { "Leave", "Add user" };
                if (chatModel.UserRole == (int)UserRole.LEAD)
                {
                    listActions.Add("Change name");
                    listActions.Add("Change admin");
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
                                _mediator.Send(new AddUserInGroupChatCommand { ChatId = chatModel.ChatId, UserTag = userTag });

                                var newUser = _mediator.Send(new GetUserByTagQuery { UserTag = userTag }).Result;

                                Chats.First(c => c.ChatId == chatModel.ChatId).Users.Add(new UserModel
                                {
                                    Email = newUser.Email,
                                    UserTag = newUser.Tag,
                                    SecondName = newUser.SecondName,
                                    FirstName = newUser.FirstName,
                                    LastName = newUser.LastName,
                                    PhoneNumber = newUser.PhoneNumber
                                });
                            }


                            break;
                        case "Leave":
                            try
                            {
                                AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");
                                if (alertDialogWindow.ShowDialog().Value)
                                {
                                    _mediator.Send(new LeaveChatCommand { ChatId = chatModel.ChatId, UserId = Properties.Settings.Default.userId });
                                }
                            }
                            catch (Exception)
                            {
                                ErrorWindow errorWindow = new ErrorWindow("Error leave chat");
                                errorWindow.ShowDialog();
                            }
                            break;

                        case "Change name":

                            InputDialogWindow changeNameDialogWindow = new InputDialogWindow("Chat name", "Change", new List<string> { chatModel.ChatName });
                            if (changeNameDialogWindow.ShowDialog().Value)
                            {
                                var name = changeNameDialogWindow.GetInputValue()[0];

                                _mediator.Send(new UpdateGroupChatCommand { ChatId = chatModel.ChatId, ChatName = name });

                                Chats.First(c => c.ChatId == chatModel.ChatId).ChatName = name;
                            }
                            break;

                        case "Change admin":

                            InputDialogWindow changeAdminDialogWindow = new InputDialogWindow("Admin tag", "Change", new List<string> { "" });
                            if (changeAdminDialogWindow.ShowDialog().Value)
                            {
                                var tag = changeAdminDialogWindow.GetInputValue()[0];

                                _mediator.Send(new UpdateGroupChatCommand { ChatId = chatModel.ChatId, AdminTag = tag });

                                Chats.First(c => c.ChatId == chatModel.ChatId).UserRole = (int)UserRole.EMPLOYEE;
                            }
                            break;
                    }
                }

            }


        }


        private class AddChatCommand : CommandBase
        {
            public override void Execute(object? parameter)
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

                                try
                                {
                                    var user = _mediator.Send(new GetUserByTagQuery { UserTag = userTag }).Result;
                                    AlertDialogWindow alertDialogWindow = new AlertDialogWindow($"Founded user: {user.FirstName} {user.SecondName} {user.LastName}", "Complete", "Cancel");
                                    if (alertDialogWindow.ShowDialog().Value)
                                    {
                                        try
                                        {
                                            var chat = _mediator.Send(new CreatePrivateChatCommand { SecondUserTag = userTag, UserId = Properties.Settings.Default.userId }).Result;
                                            MessengerVM messengerVM = MessengerVM.GetInstance(_mediator);
                                            messengerVM.Chats.Add(chat);

                                        }
                                        catch
                                        {
                                            ErrorWindow errorWindow = new ErrorWindow("Error creating private chat");
                                        }
                                    }

                                }
                                catch
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("User not found");
                                    errorWindow.ShowDialog();
                                }

                            }


                            break;
                        case "Create group chat":


                            InputDialogWindow windowGroup = new InputDialogWindow("Enter group name", "Create", new List<string> { "Name" });
                            if (windowGroup.ShowDialog().Value)
                            {
                                string nameGroup = windowGroup.GetInputValue()[0];

                                try
                                {
                                    var chat = _mediator.Send(new CreateGroupChatCommand { Name = nameGroup, UserId = Properties.Settings.Default.userId }).Result;
                                    MessengerVM messengerVM = MessengerVM.GetInstance(_mediator);
                                    messengerVM.Chats.Add(chat);

                                }
                                catch
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("Error creating group chat");
                                }


                            }
                            break;
                    }
                }
            }
        }
    }
}
