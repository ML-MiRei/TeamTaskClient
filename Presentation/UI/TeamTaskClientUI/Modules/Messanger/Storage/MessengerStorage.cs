using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.UI.Modules.Messanger.Storage
{
    internal class MessengerStorage
    {
        static MessengerStorage()
        {
            _messages.CollectionChanged += OnMessagesCollectionChanged;
        }



        private static void OnMessagesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                SelectedChat.Messages.Add((MessageModel)sender);
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                SelectedChat.Messages.Remove((MessageModel)sender);

            SelectedChatChanged?.Invoke(sender, null);
        }

        private static ObservableCollection<ChatModel> _chats;
        public static ObservableCollection<ChatModel> Chats
        {
            get => _chats;

            set
            {
                _chats = value;
            }
        }



        private static ChatModel _selectedChat { get; set; }
        public static ChatModel SelectedChat
        {
            get => _selectedChat;
            set
            {
                _selectedChat = value;


                if (value != null && value.Messages != null)
                    Messages = new ObservableCollection<MessageModel>(value.Messages);

                SelectedChatChanged?.Invoke(SelectedChatChanged, value);
            }
        }



        private static ObservableCollection<MessageModel> _messages = new ObservableCollection<MessageModel>();
        public static ObservableCollection<MessageModel> Messages
        {
            get => _messages;

            set
            {
                _messages = value;
            }
        }



        public static event EventHandler<ChatModel> SelectedChatChanged;
    }
}
