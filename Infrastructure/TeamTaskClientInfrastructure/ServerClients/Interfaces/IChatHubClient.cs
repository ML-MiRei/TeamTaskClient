using Microsoft.AspNetCore.SignalR.Client;

namespace TeamTaskClient.Infrastructure.ServerClients.Interfaces
{
    public interface IChatHubClient
    {
        HubConnection GetClient();
    }
}
