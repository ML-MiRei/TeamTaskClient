using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.DeleteMessage;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.UpdateMessage;
using TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.UI.Modules.Messanger.View;
using TeamTaskClient.UI.UserControls;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class MessagesPageVM : MessengerVM
    {

        private static IMediator _mediator;

        public static string WatermarkText { get => "Enter message.."; }

        public MessagesPageVM(IMediator mediator) : base(mediator)
        {

            _mediator = mediator;
            InputMessage = WatermarkText;
            OnChatSelected += MessagesPageVM_OnChatSelected;
            SendMessage = new SendMessageButton(this);
            DoubleClick = new DoubleClickCommand(this);

            ChatService.OnMessageReceived += ChatService_OnMessageReceived;
            ChatService.OnMessageDeleted += ChatService_OnMessageDeleted;
        }

        private void ChatService_OnMessageDeleted(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Messages));
        }

        private void ChatService_OnMessageReceived(object? sender, MessageModel e)
        {
            OnPropertyChanged(nameof(Messages));
        }


        private void MessagesPageVM_OnChatSelected(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Messages));

        }


        

        public ObservableCollection<MessageModel> Messages
        {
            get {

                if (SelectedChat == null)
                    return new ObservableCollection<MessageModel>(); 

                return new ObservableCollection<MessageModel>(Chats.First(c => c.ChatId == SelectedChat).Messages); 
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
            public override void Execute(object? parameter)
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
                                _mediator.Send(new UpdateMessageCommand { ChatId = _selectedChat.Value, MessageId = ((MessageModel)parameter).MessageId, TextMessage = newTextMessage });
                                vm.Messages.First(m => m.MessageId == ((MessageModel)parameter).MessageId).TextMessage = newTextMessage;
                                vm.OnPropertyChanged(nameof(Messages));
                            }


                            break;
                        case "Delete":
                            try
                            {
                                _mediator.Send(new DeleteMessageCommand() { ChatId = _selectedChat.Value, MessageId = ((MessageModel)parameter).MessageId });
                                vm.Messages.Remove(vm.Messages.First(m => m.MessageId == ((MessageModel)parameter).MessageId));
                            }
                            catch (Exception)
                            {
                                ErrorWindow errorWindow = new ErrorWindow("Error delete message");
                                errorWindow.ShowDialog();
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
                if(_inputMessage != "" && _inputMessage != WatermarkText)
                {
                    await _mediator.Send(new SendMessageCommand
                    {
                        ChatId = _selectedChat.Value,
                        TextMessage = _inputMessage.Trim(),
                        UderId = Properties.Settings.Default.userId
                    });
                    messagesPageVM.InputMessage = "Enter message..";
                }

              
            }
        }
    }
}
