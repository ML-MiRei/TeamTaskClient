using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Implementation
{
    public class ProjectHubConnection : IProjectHubConnection
    {

        private static HubConnection _hubConnection;
        public HubConnection HubConnection => _hubConnection;


        public ProjectHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/online-projects")
            .Build();

            _hubConnection.StartAsync();
        }


        public void Dispose()
        {
            _hubConnection.StopAsync();
        }
    }
}
