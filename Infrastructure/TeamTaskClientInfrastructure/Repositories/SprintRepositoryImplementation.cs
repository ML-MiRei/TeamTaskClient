using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class SprintRepositoryImplementation(IHttpClient client, IProjectHubConnection projectHubConnection) : ISprintRepositoryInterface
    {

        private HubConnection HubClient = projectHubConnection.HubConnection;


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
