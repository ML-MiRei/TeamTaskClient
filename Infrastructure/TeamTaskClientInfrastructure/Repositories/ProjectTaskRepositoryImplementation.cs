using Microsoft.AspNetCore.SignalR.Client;
using System.Net;
using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.LocalDB.Models;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectTaskRepositoryImplementation(IHttpClient client) : IProjectTaskRepository
    {
        private HubConnection HubClient = ProjectHubConnection.Instance.GetClient();


        public async Task ChangeStatusProjectTask(int projectId, int projectTaskId, int status)
        {
            await HubClient.SendAsync("ChangeStatusProjectTask", new ProjectTaskEntity
            {
                Status = status,
                ProjectId = projectId,
                ID = projectTaskId
            });
        }

        public async Task CreateProjectTask(ProjectTaskEntity entity)
        {
            await HubClient.SendAsync("ChangeStatusProjectTask", entity);

        }

        public async Task DeleteProjectTask(int projectId, int projectTaskId)
        {

            await HubClient.SendAsync("DeleteProjectTask", projectId, projectTaskId);

        }

        public async Task SetExecutorProjectTask(int projectId, int projectTaskId, string userTag)
        {
            await HubClient.SendAsync("SetExecutorProjectTask", projectId, projectTaskId, userTag);

        }


        public async Task AddInSprintProjectTask(int projectTaskId, int sprintId)
        {
            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/ProjectTask/{projectTaskId}/add-in-sprint", JsonContent.Create(sprintId));

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return;
            }
            throw new ConnectionException();
        }

        public async Task UpdateProjectTask(ProjectTaskEntity projectTask)
        {

            await HubClient.SendAsync("UpdateProjectTask", projectTask);

        }
    }
}
