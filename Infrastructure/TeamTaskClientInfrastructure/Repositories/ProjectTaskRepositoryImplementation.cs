using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectTaskRepositoryImplementation(IHttpClient client, IProjectHubConnection projectHubConnection) : IProjectTaskRepository
    {
        private HubConnection HubClient = projectHubConnection.HubConnection;


        public async Task ChangeStatusProjectTask(int projectId, int spintId, int projectTaskId, int status)
        {
            await HubClient.SendAsync("ChangeStatusProjectTask", new ProjectTaskEntity
            {
                Status = status,
                ProjectId = projectId,
                ID = projectTaskId,
                SprintId = spintId
            });
        }

        public async Task CreateProjectTask(ProjectTaskEntity entity)
        {
            await HubClient.SendAsync("CreateProjectTask", entity);

        }

        public async Task DeleteProjectTask(int projectId, int projectTaskId)
        {

            await HubClient.SendAsync("DeleteProjectTask", projectId, projectTaskId);

        }

        public async Task SetExecutorProjectTask(int projectId, int projectTaskId, string userTag)
        {
            await HubClient.SendAsync("SetExecutorProjectTask", projectId, projectTaskId, userTag);

        }


        public async Task AddInSprintProjectTask(int projectId, int sprintId, int projectTaskId)
        {

            await HubClient.SendAsync("AddInSprintProjectTask", projectId, sprintId, projectTaskId);


        }

        public async Task UpdateProjectTask(ProjectTaskEntity projectTask)
        {

            await HubClient.SendAsync("UpdateProjectTask", projectTask);

        }
    }
}
