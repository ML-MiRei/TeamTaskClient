using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.DeleteMessage;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.UpdateMessage;
using TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class MessagesPageVM : ViewModelBase
    {

        private static IMediator _mediator;

        public static string WatermarkText { get => "Enter message.."; }

        public MessagesPageVM(IMediator mediator) 
        {

            _mediator = mediator;
            InputMessage = WatermarkText;
            SendMessage = new SendMessageButton(this);
            DoubleClick = new DoubleClickCommand(this);


            MessengerStorage.SelectedChatChanged += OnSelectedChatChanged;
            ChatHubClient.OnMessageReceived += ChatService_OnMessageReceived;
            ChatHubClient.OnMessageDeleted += ChatService_OnMessageDeleted;
            ChatHubClient.OnMessageUpdated += ChatService_OnMessageUpdated;
        }

        private void ChatService_OnMessageUpdated(object? sender, string e)
        {
           App.Current.Dispatcher.Invoke(() => MessengerStorage.Messages.First(m => m.MessageId == (int)sender).TextMessage = e);
            OnPropertyChanged(nameof(Messages));
        }

        private void OnSelectedChatChanged(object? sender, ChatModel e)
        {
            OnPropertyChanged(nameof(Messages));
        }

        private void ChatService_OnMessageDeleted(object? sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() => MessengerStorage.Messages.Remove(MessengerStorage.Messages.First(m => m.MessageId == (int)sender)));
            OnPropertyChanged(nameof(Messages));
        }

        private void ChatService_OnMessageReceived(object? sender, MessageModel e)
        {
            App.Current.Dispatcher.Invoke(() => MessengerStorage.AddMessage((int)sender, e));
            OnPropertyChanged(nameof(Messages));
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
                                vm.Messages.First(m => m.MessageId == ((MessageModel)parameter).MessageId).TextMessage = newTextMessage;
                                vm.OnPropertyChanged(nameof(Messages));
                            }


                            break;
                        case "Delete":
                            try
                            {
                               await _mediator.Send(new DeleteMessageCommand() { ChatId = MessengerStorage.SelectedChat.ChatId, MessageId = ((MessageModel)parameter).MessageId });
                                vm.Messages.Remove(vm.Messages.First(m => m.MessageId == ((MessageModel)parameter).MessageId));
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
