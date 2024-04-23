using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Connections
{
    public class TeamHubConnection : IDisposable
    {

        private static HubConnection _hubConnection;

        private static TeamHubConnection _instance;
        public static TeamHubConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TeamHubConnection();

                return _instance;
            }
        }


        private TeamHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7130/online-teams")
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
