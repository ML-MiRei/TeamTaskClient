using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Connections
{
    public class ChatHubConnection : IDisposable
    {

        private static HubConnection _hubConnection;

        private static ChatHubConnection _instance;
        public static ChatHubConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatHubConnection();

                return _instance;
            }
        }


        private ChatHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/online-chat")
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
