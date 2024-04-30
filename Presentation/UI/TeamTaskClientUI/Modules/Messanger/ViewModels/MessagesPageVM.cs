using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Message.Commands.DeleteMessage;
using TeamTaskClient.ApplicationLayer.UseCases.Message.Commands.UpdateMessage;
using TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class MessagesPageVM : ViewModelBase
    {

        private static IMediator _mediator;
        public event EventHandler InterfaceRefresh;

        public static string WatermarkText { get => "Enter message.."; }

        public MessagesPageVM(IMediator mediator, IMessengerEvents messengerEvents)
        {

            _mediator = mediator;
            InputMessage = WatermarkText;
            SendMessage = new SendMessageButton(this);
            DoubleClick = new DoubleClickCommand(this);


            MessengerStorage.SelectedChatChanged += OnSelectedChatChanged;
            messengerEvents.OnMessageReceived += OnMessageReceived;
            messengerEvents.OnMessageDeleted += OnMessageDeleted;
            messengerEvents.OnMessageUpdated += OnMessageUpdated;
        }




        private void OnMessageUpdated(object? sender, MessageEntity e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (MessengerStorage.SelectedChat.ChatId == e.ChatId)
                {
                    MessengerStorage.UpdateMessage(e.ChatId, new MessageModel { TextMessage = e.TextMessage, MessageId = e.ID });
                }
                MessengerStorage.Messages.First(m => m.MessageId == e.ID).TextMessage = e.TextMessage;
            });
            InterfaceRefresh?.Invoke(null, new EventArgs());

        }

        private void OnSelectedChatChanged(object? sender, ChatModel e)
        {
            OnPropertyChanged(nameof(Messages));
        }

        private void OnMessageDeleted(object? sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() => MessengerStorage.Messages.Remove(MessengerStorage.Messages.First(m => m.MessageId == (int)sender)));
        }

        private void OnMessageReceived(object? sender, MessageModel e)
        {
            App.Current.Dispatcher.Invoke(() => MessengerStorage.AddMessage((int)sender, e));
        }






        public ObservableCollection<MessageModel> Messages
        {
            get
            {

                return MessengerStorage.Messages;
            }

        }


        private static string _inputMessage;
        public string InputMessage
        {
            get
            {
                return _inputMessage;
            }
            set
            {
                _inputMessage = value;
                OnPropertyChanged(nameof(InputMessage));
            }
        }



        public ICommand SendMessage { get; }
        public ICommand DoubleClick { get; }


        private class DoubleClickCommand(MessagesPageVM vm) : CommandBase
        {
            public async override void Execute(object? parameter)
            {

                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Update", "Delete" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    switch (SelectActionsDialogVM.SelectedAction)
                    {
                        case "Update":

                            InputDialogWindow updatePropertiesDialogWindow = new InputDialogWindow("Update message", "Save", new List<string> { ((MessageModel)parameter).TextMessage });
                            if (updatePropertiesDialogWindow.ShowDialog().Value)
                            {
                                var newTextMessage = updatePropertiesDialogWindow.GetInputValue()[0];
                                await _mediator.Send(new UpdateMessageCommand { ChatId = MessengerStorage.SelectedChat.ChatId, MessageId = ((MessageModel)parameter).MessageId, TextMessage = newTextMessage });
                            }


                            break;
                        case "Delete":
                            try
                            {
                                await _mediator.Send(new DeleteMessageCommand() { ChatId = MessengerStorage.SelectedChat.ChatId, MessageId = ((MessageModel)parameter).MessageId });
                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error delete message");
                            }
                            break;
                    }
                }
            }
        }


        private class SendMessageButton(MessagesPageVM messagesPageVM) : CommandBase
        {
            public override async void Execute(object? parameter)
            {
                if (_inputMessage != "" && _inputMessage != WatermarkText)
                {
                    await _mediator.Send(new SendMessageCommand
                    {
                        ChatId = MessengerStorage.SelectedChat.ChatId,
                        TextMessage = _inputMessage.Trim(),
                        UderId = Properties.Settings.Default.userId
                    });
                    messagesPageVM.InputMessage = "Enter message..";
                }


            }
        }
    }
}
