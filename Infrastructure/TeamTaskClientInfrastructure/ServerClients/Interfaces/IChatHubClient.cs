using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.Infrastructure.ServerClients.Interfaces
{
    public interface IChatHubClient
    {
        HubConnection GetClient();
    }
}
