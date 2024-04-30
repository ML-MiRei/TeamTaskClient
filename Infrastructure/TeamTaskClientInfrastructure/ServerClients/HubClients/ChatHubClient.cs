using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class ChatHubClient : IMessengerEvents
    {
        private HubConnection _hubConnection;



        private static ChatHubClient _instance;
        public static ChatHubClient GetInstance()
        {
            return _instance;
        }



        public ChatHubClient(IChatHubConnection chatHubConnection, int userId, string userTag)
        {
            _instance = this;
            _hubConnection = chatHubConnection.HubConnection;

            _hubConnection.SendAsync("ConnectUserWithChats", userId, userTag).Wait();


            _hubConnection.On<int, MessageModel>("Receive", (chatId, messageModel) =>
            {
                OnMessageReceived?.Invoke(chatId, messageModel);
            });


            _hubConnection.On<MessageEntity>("MessageUpdated", (messageEntity) =>
            {
                OnMessageUpdated?.Invoke(null, messageEntity);
            });


            _hubConnection.On<int>("MessageIsDeleted", (messageId) =>
            {
                OnMessageDeleted?.Invoke(messageId, new EventArgs());
            });


            _hubConnection.On<ChatModel?>("NewChatReply", (chatModel) =>
            {
                PrivateChatCreated?.Invoke(null, chatModel);
            });

            _hubConnection.On<ChatModel?>("NewGroupChatReply", (chatModel) =>
            {
                GroupChatCreated?.Invoke(null, chatModel);
            });


            _hubConnection.On<int, UserModel>("AddUserInChatReply", (chatId, userModel) =>
            {
                AddNewUserChat?.Invoke(chatId, userModel);
            });

            _hubConnection.On<int, string>("DeleteUserFromChatReply", (chatId, userTag) =>
            {
                DeleteUserFromChat?.Invoke(chatId, userTag);
            });

            _hubConnection.On<ChatModel>("UpdateChatReply", (chatModel) =>
            {
                ChatUpdated?.Invoke(null, chatModel);
            });


            _hubConnection.On<int>("DeleteChatReply", (chatId) =>
            {
                DeleteChat?.Invoke(null, chatId);
            });


            _hubConnection.On<NotificationModel>("NotificationReply", (notificationModel) =>
            {
                NotificationAdded?.Invoke(null, notificationModel);
            });

        }


        public event EventHandler<MessageModel> OnMessageReceived;
        public event EventHandler OnMessageDeleted;
        public event EventHandler<MessageEntity> OnMessageUpdated;
        public event EventHandler<ChatModel> PrivateChatCreated;
        public event EventHandler<ChatModel> GroupChatCreated;
        public event EventHandler<ChatModel> ChatUpdated;
        public event EventHandler<UserModel> AddNewUserChat;
        public event EventHandler<string> DeleteUserFromChat;
        public event EventHandler<int> DeleteChat;

        public event EventHandler<NotificationModel> NotificationAdded;
    }
}
