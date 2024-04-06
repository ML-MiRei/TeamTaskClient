using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreateGroupChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreatePrivateChat;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
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
