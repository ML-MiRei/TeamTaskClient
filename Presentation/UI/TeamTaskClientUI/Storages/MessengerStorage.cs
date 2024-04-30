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

namespace TeamTaskClient.UI.Storages
{
    public class MessengerStorage
    {
        public MessengerStorage(IMessengerEvents messengerEvents)
        {
            messengerEvents.PrivateChatCreated += OnPrivateChatCreated;
            messengerEvents.AddNewUserChat += OnAddNewUserChat;
            messengerEvents.ChatUpdated += OnChatUpdated;
            messengerEvents.DeleteUserFromChat += OnDeleteUserFromChat;
            messengerEvents.DeleteChat += OnDeleteChat;
            messengerEvents.GroupChatCreated += OnGroupChatCreated;
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



        public static void AddChat(ChatModel chatModel)
        {
            _chats.Add(chatModel);
            SelectedChat = chatModel;
            SelectedChatChanged?.Invoke(null, chatModel);
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


        public static void AddMessage(int chatId, MessageModel messageModel)
        {
            if (SelectedChat?.ChatId == chatId)
            {
                _messages.Add(messageModel);
                ChatsRefresh?.Invoke(chatId, new EventArgs());
            }

            Chats.First(c => c.ChatId == chatId).Messages.Add(messageModel);
        }


        public static void UpdateMessage(int chatId, MessageModel messageModel)
        {
            if (SelectedChat?.ChatId == chatId)
            {
                _messages.First(m => m.MessageId == messageModel.MessageId).TextMessage = messageModel.TextMessage;
            }

            Chats.First(c => c.ChatId == chatId).Messages.First(m => m.MessageId == messageModel.MessageId).TextMessage = messageModel.TextMessage;
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
