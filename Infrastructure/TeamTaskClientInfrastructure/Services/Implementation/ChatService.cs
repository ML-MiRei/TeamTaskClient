using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.Implementation;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Services.Implementation
{
    public class ChatService
    {
        public HubConnection HubClient;


        private static ChatService _instance;
        public static ChatService GetInstance(int userId)
        {

            if (_instance == null)
                _instance = new ChatService(userId);
            return _instance;

        }
        public static ChatService GetInstance()
        {
            return _instance;
        }


        private ChatService(int userId)
        {
            IChatHubClient chatHubClient = ChatHubClient.Instance;

            HubClient = chatHubClient.GetClient();

            HubClient.SendAsync("ConnectUserWithChats", userId).Wait();


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
        }


        public static event EventHandler<MessageModel> OnMessageReceived;
        public static event EventHandler OnMessageDeleted;
        public static event EventHandler<string> OnMessageUpdated;
    }
}
