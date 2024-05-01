using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class ChatHubReplies : IMessengerEvents
    {
        private HubConnection _hubConnection;


        public ChatHubReplies(IChatHubConnection chatHubConnection, int userId, string userTag)
        {
            _hubConnection = chatHubConnection.HubConnection;

            _hubConnection.SendAsync("ConnectUserWithChats", userId, userTag).Wait();


            _hubConnection.On<int, MessageModel>("Receive", (chatId, messageModel) =>
            {
                MessageReceived?.Invoke(chatId, messageModel);
            });


            _hubConnection.On<MessageEntity>("MessageUpdated", (messageEntity) =>
            {
                MessageUpdated?.Invoke(null, messageEntity);
            });


            _hubConnection.On<int>("MessageIsDeleted", (messageId) =>
            {
                MessageDeleted?.Invoke(messageId, new EventArgs());
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
                UserFromChatDeleted?.Invoke(chatId, userTag);
            });

            _hubConnection.On<ChatModel>("UpdateChatReply", (chatModel) =>
            {
                ChatUpdated?.Invoke(null, chatModel);
            });


            _hubConnection.On<int>("DeleteChatReply", (chatId) =>
            {
                ChatDeleted?.Invoke(null, chatId);
            });


            _hubConnection.On<NotificationModel>("NotificationReply", (notificationModel) =>
            {
                NotificationAdded?.Invoke(null, notificationModel);
            });

        }


        public event EventHandler<MessageModel> MessageReceived;
        public event EventHandler MessageDeleted;
        public event EventHandler<MessageEntity> MessageUpdated;
        public event EventHandler<ChatModel> PrivateChatCreated;
        public event EventHandler<ChatModel> GroupChatCreated;
        public event EventHandler<ChatModel> ChatUpdated;
        public event EventHandler<UserModel> AddNewUserChat;
        public event EventHandler<string> UserFromChatDeleted;
        public event EventHandler<int> ChatDeleted;

        public event EventHandler<NotificationModel> NotificationAdded;
    }
}
