using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class SprintRepositoryImplementation(IHttpClient client) : ISprintRepositoryInterface
    {

        private HubConnection HubClient = ProjectHubConnection.Instance.GetClient();


        public async Task ChangeDateEndSprint(int projectId, int sprintId, DateTime dateEnd)
        {
            await HubClient.SendAsync("UpdateDateEndSprint", projectId, sprintId, dateEnd);

        }

        public async Task ChangeDateStartSprint(int projectId, int sprintId, DateTime dateStart)
        {

            await HubClient.SendAsync("UpdateDateStartSprint", projectId, sprintId, dateStart);

        }

        public async Task CreateSprint(int projectId, SprintModel sprintModel)
        {
            await HubClient.SendAsync("CreateSprint", projectId, sprintModel);

        }

        public async Task DeleteSprint(int projectId, int sprintId)
        {
            await HubClient.SendAsync("DeleteSprint", projectId, sprintId);

        }
    }
}
