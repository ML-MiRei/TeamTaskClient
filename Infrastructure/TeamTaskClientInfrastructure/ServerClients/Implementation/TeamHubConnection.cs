using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Implementation
{
    public class TeamHubConnection : ITeamHubConnection
    {

        private static HubConnection _hubConnection;
        public HubConnection HubConnection => _hubConnection;

        public TeamHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/online-teams")
            .Build();

            _hubConnection.StartAsync();
        }

        public void Dispose()
        {
            _hubConnection.StopAsync();
        }
    }
}
