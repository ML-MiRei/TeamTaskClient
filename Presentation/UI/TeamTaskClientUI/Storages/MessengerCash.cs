using AutoMapper;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Modules.Profile.ViewModels;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Storages
{
    public class MessengerCash : IMessengerCash
    {
        public MessengerCash(IMessengerEvents messengerEvents)
        {
            messengerEvents.PrivateChatCreated += OnPrivateChatCreated;
            messengerEvents.AddNewUserChat += OnAddNewUserChat;
            messengerEvents.ChatUpdated += OnChatUpdated;
            messengerEvents.UserFromChatDeleted += OnDeleteUserFromChat;
            messengerEvents.ChatDeleted += OnDeleteChat;
            messengerEvents.GroupChatCreated += OnGroupChatCreated;

            messengerEvents.MessageDeleted += OnMessageDeleted;
            messengerEvents.MessageReceived += OnMessageReceived;
            messengerEvents.MessageUpdated += OnMessageUpdated;
        }

        private void OnMessageUpdated(object? sender, Domain.Entities.MessageEntity e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (SelectedChat?.ChatId == (int)sender)
                {
                    _messages.First(m => m.MessageId == e.ID).TextMessage = e.TextMessage;
                    ChatsRefresh?.Invoke((int)sender, new EventArgs());
                }

                Chats.First(c => c.ChatId == (int)sender).Messages.First(m => m.MessageId == e.ID).TextMessage = e.TextMessage;
            });
        }

        private void OnMessageReceived(object? sender, MessageModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (SelectedChat?.ChatId == (int)sender)
                {
                    _messages.Add(e);
                    ChatsRefresh?.Invoke((int)sender, new EventArgs());
                }

                Chats.First(c => c.ChatId == (int)sender).Messages.Add(e);
            });
        }

        private void OnMessageDeleted(object? sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() => Messages.Remove(Messages.First(m => m.MessageId == (int)sender)));
        }

        public static event EventHandler ChatsRefresh;


        private static void OnGroupChatCreated(object? sender, ChatModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.Add(e));
        }

        private static void OnDeleteChat(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.Remove(_chats.First(c => c.ChatId == e)));
        }

        private static void OnDeleteUserFromChat(object? sender, string e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == (int)sender)
            .Users
            .Remove(_chats.First(c => c.ChatId == (int)sender).Users.First(u => u.UserTag == e)));
        }

        private static void OnChatUpdated(object? sender, ChatModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == e.ChatId).ChatName = e.ChatName);
            ChatsRefresh?.Invoke(null, new EventArgs());
        }

        private static void OnAddNewUserChat(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == (int)sender).Users.Add(e));
        }

        private static void OnPrivateChatCreated(object? sender, ChatModel e)
        {
            e.ChatName = e.Users.First(u => u.UserTag != Properties.Settings.Default.userTag).FirstName;
            App.Current.Dispatcher.Invoke(() => _chats.Add(e));
        }




        private static ObservableCollection<ChatModel> _chats;
        public ObservableCollection<ChatModel> Chats
        {
            get => _chats;

            set
            {
                _chats = value;
            }
        }





        private static ChatModel _selectedChat { get; set; }
        public ChatModel SelectedChat
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




        public void UpdateMessage(int chatId, MessageModel messageModel)
        {
            if (SelectedChat?.ChatId == chatId)
            {
                _messages.First(m => m.MessageId == messageModel.MessageId).TextMessage = messageModel.TextMessage;
            }

            Chats.First(c => c.ChatId == chatId).Messages.First(m => m.MessageId == messageModel.MessageId).TextMessage = messageModel.TextMessage;
        }


        private static ObservableCollection<MessageModel> _messages = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> Messages
        {
            get => _messages;

            set
            {
                _messages = value;
            }
        }



        public event EventHandler<ChatModel> SelectedChatChanged;
    }
}
