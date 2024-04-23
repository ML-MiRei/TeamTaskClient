using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Connections
{
    public class ProjectHubConnection : IDisposable
    {

        private static HubConnection _hubConnection;

        private static ProjectHubConnection _instance;
        public static ProjectHubConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProjectHubConnection();

                return _instance;
            }
        }


        private ProjectHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/online-projects")
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
