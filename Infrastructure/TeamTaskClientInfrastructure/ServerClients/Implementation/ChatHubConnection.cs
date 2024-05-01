using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Implementation
{
    public class ChatHubConnection : IChatHubConnection
    {

        private static HubConnection _hubConnection;

        public HubConnection HubConnection => _hubConnection;

        public ChatHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/online-chat")
            .Build();

            _hubConnection.StartAsync();
        }

        public void Dispose()
        {
            _hubConnection.StopAsync();
        }
    }
}
