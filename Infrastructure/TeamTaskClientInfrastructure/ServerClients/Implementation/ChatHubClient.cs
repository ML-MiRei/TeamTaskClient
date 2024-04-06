﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Implementation
{
    internal class ChatHubClient : IChatHubClient, IDisposable
    {

        private static HubConnection _hubConnection;

        private static ChatHubClient _instance;
        public static ChatHubClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatHubClient();

                return _instance;
            }
        }


        private ChatHubClient()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/chat")
                .Build();

            _hubConnection.StartAsync();
        }



        public HubConnection GetClient()
        {
            return _hubConnection;
        }

        public void Dispose()
        {
            _hubConnection.StopAsync();
        }
    }
}