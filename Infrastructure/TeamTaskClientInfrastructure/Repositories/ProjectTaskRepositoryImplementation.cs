using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectTaskRepositoryImplementation(IHttpClient client) : IProjectTaskRepository
    {
        public async Task ChangeStatusProjectTask(int projectId, int projectTaskId, int status)
        {
            var content = JsonContent.Create((int)status);


            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/ProjectTask/{projectId}/{projectTaskId}/change-status", content);

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

        public async Task<ProjectTaskModel> CreateProjectTask(ProjectTaskEntity entity)
        {
            var content = JsonContent.Create(entity);


            var httpReply =  client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/ProjectTask/create", content).Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return await httpReply.Content.ReadFromJsonAsync<ProjectTaskModel>();
            }
            throw new ConnectionException();
        }

        public async Task DeleteProjectTask(int projectId, int projectTaskId)
        {
            var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/ProjectTask/{projectTaskId}/delete");

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

        public async Task SetExecutorProjectTask(int projectTaskId, string userTag)
        {
            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/ProjectTask/{projectTaskId}/set-executor", JsonContent.Create(userTag));

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
            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/ProjectTask/{projectTask.ID}/update", JsonContent.Create(projectTask));

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
    }
}
