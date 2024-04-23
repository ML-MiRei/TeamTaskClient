using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Implementation
{
    public class NotificationHubClient : INotificationHubClient, IDisposable
    {

        private static HubConnection _hubConnection;

        private static NotificationHubClient _instance;
        public static NotificationHubClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotificationHubClient();

                return _instance;
            }
        }


        private NotificationHubClient()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/notification")
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
