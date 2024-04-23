using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class ChatHubClient
    {
        public HubConnection HubClient;


        private static ChatHubClient _instance;
        public static ChatHubClient GetInstance(int userId, string userTag)
        {

            if (_instance == null)
                _instance = new ChatHubClient(userId, userTag);
            return _instance;

        }
        public static ChatHubClient GetInstance()
        {
            return _instance;
        }


        private ChatHubClient(int userId, string userTag)
        {
            ChatHubConnection chatHubClient = ChatHubConnection.Instance;

            HubClient = chatHubClient.GetClient();

            HubClient.SendAsync("ConnectUserWithChats", userId, userTag).Wait();


            HubClient.On<int, MessageModel>("Receive", (chatId, messageModel) =>
            {
                OnMessageReceived?.Invoke(chatId, messageModel);
            });


            HubClient.On<int, string>("MessageIsUpdated", (messageId, textMessage) =>
            {
                OnMessageUpdated?.Invoke(messageId, textMessage);
            });


            HubClient.On<int>("MessageIsDeleted", (messageId) =>
            {
                OnMessageDeleted?.Invoke(messageId, new EventArgs());
            });


            HubClient.On<ChatModel?>("NewChatReply", (chatModel) =>
            {
                PrivateChatCreated?.Invoke(null, chatModel);
            });


            HubClient.On<int, UserModel>("AddUserInChatReply", (chatId, userModel) =>
            {
                AddNewUserChat?.Invoke(chatId, userModel);
            });

            HubClient.On<int, string>("DeleteUserFromChatReply", (chatId, userTag) =>
            {
                DeleteUserFromChat?.Invoke(chatId, userTag);
            });

            HubClient.On<ChatModel>("UpdateChatReply", (chatModel) =>
            {
                ChatUpdated?.Invoke(null, chatModel);
            });


            HubClient.On<int>("DeleteChatReply", (chatId) =>
            {
                DeleteChat?.Invoke(null, chatId);
            });
        }


        public static event EventHandler<MessageModel> OnMessageReceived;
        public static event EventHandler OnMessageDeleted;
        public static event EventHandler<string> OnMessageUpdated;
        public static event EventHandler<ChatModel> PrivateChatCreated;
        public static event EventHandler<ChatModel> ChatUpdated;
        public static event EventHandler<UserModel> AddNewUserChat;
        public static event EventHandler<string> DeleteUserFromChat;
        public static event EventHandler<int> DeleteChat;
    }
}
