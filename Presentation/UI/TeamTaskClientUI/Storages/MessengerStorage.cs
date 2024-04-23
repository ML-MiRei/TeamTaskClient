using AutoMapper;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Modules.Profile.ViewModels;

namespace TeamTaskClient.UI.Storages
{
    public class MessengerStorage
    {
        static MessengerStorage()
        {
            _messages.CollectionChanged += OnMessagesCollectionChanged;


            ChatHubClient.PrivateChatCreated += OnPrivateChatCreated;
            ChatHubClient.AddNewUserChat += OnAddNewUserChat;
            ChatHubClient.ChatUpdated += OnChatUpdated;
            ChatHubClient.DeleteUserFromChat += OnDeleteUserFromChat;
            ChatHubClient.DeleteChat += OnDeleteChat; ;
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
        }

        private static void OnAddNewUserChat(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == (int)sender).Users.Add(e));
        }

        private static void OnPrivateChatCreated(object? sender, ChatModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.Add(e));
        }

        private static void OnMemberChatAdded(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == (int)sender).Users.Add(e));
            ProfileVM.profile.UpdateNotifications();
        }

        private static void OnMemberChatDeleted(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == (int)sender).Users.Remove(e));
            ProfileVM.profile.UpdateNotifications();

        }

        private static void OnChatDelete(object? sender, ChatModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.Remove(e));
            ProfileVM.profile.UpdateNotifications();

        }

        private static void OnChatCreate(object? sender, ChatModel e)
        {
            App.Current.Dispatcher.Invoke(() => AddChat(e));
            ProfileVM.profile.UpdateNotifications();

        }

        private static void OnChatUpdate(object? sender, ChatModel e)
        {
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == e.ChatId).ChatName = e.ChatName);
            App.Current.Dispatcher.Invoke(() => _chats.First(c => c.ChatId == e.ChatId).UserRole = e.UserRole);
            ProfileVM.profile.UpdateNotifications();

        }

        private static void OnMessagesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                SelectedChat.Messages.Add((MessageModel)sender);
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                SelectedChat.Messages.Remove((MessageModel)sender);

            SelectedChatChanged?.Invoke(sender, null);
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
            if (SelectedChat.ChatId == chatId)
            {
                _messages.Add(messageModel);
                SelectedChat.Messages.Add(messageModel);
                SelectedChatChanged?.Invoke(SelectedChatChanged, null);
            }


            Chats.First(c => c.ChatId == SelectedChat.ChatId).Messages.Add(messageModel);


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
