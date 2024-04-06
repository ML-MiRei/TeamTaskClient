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
        public static ChatService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatService();
                return _instance;
            }
        }



        private ChatService()
        {
            IChatHubClient chatHubClient = ChatHubClient.Instance;

            HubClient = chatHubClient.GetClient();

            

            HubClient.On<int, MessageModel>("Receive", (chatId, messageModel) =>
            {
                OnMessageReceived?.Invoke(chatId, messageModel);
            });
        }


        public static event EventHandler<MessageModel> OnMessageReceived;
    }
}
