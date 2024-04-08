using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Common.Base;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    class MessengerVM : ViewModelBase
    {

        private static MessengerVM _instance;
        public static MessengerVM GetInstance(IMediator mediator)
        {

            if (_instance == null)
                _instance = new MessengerVM(mediator);
            return _instance;
        }

        public MessengerVM(IMediator mediator)
        {
            _chats = new ObservableCollection<ChatModel>(mediator.Send(new GetChatsQuery() { UserId = Properties.Settings.Default.userId }).Result.ToList());


            ChatService.OnMessageReceived += ChatService_OnMessageReceived;
            ChatService.OnMessageDeleted += ChatService_OnMessageDeleted;

            _chats.CollectionChanged += _chats_CollectionChanged;

        }

        private void ChatService_OnMessageDeleted(object? sender, EventArgs e)
        {
            var chat = Chats.First(c => c.ChatId == SelectedChat);
            chat.Messages.Remove(chat.Messages.First(m => m.MessageId == (int)sender));
        }

        private void _chats_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Chats));
        }

        private void ChatService_OnMessageReceived(object? sender, MessageModel e)
        {
            Chats.First(c => c.ChatId == (int)sender).Messages.Add(e);
        }



        public static event EventHandler OnChatSelected;

        protected ObservableCollection<ChatModel> _chats;
        public ObservableCollection<ChatModel> Chats
        {
            get { return _chats; }
            set { _chats = value; }
        }


        protected static int? _selectedChat;
        public int? SelectedChat
        {
            get { return _selectedChat; }
            set
            {
                _selectedChat = value;
                OnChatSelected?.Invoke(value, new EventArgs());
            }
        }
    }
}
