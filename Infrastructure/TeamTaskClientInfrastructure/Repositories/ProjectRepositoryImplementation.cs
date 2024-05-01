using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectRepositoryImplementation(IHttpClient client, IProjectHubConnection projectHubConnection) : IProjectRepository
    {

        private HubConnection HubClient = projectHubConnection.HubConnection;


        public async Task AddTeamInProject(int projectId, string teamTag)
        {
            await HubClient.SendAsync("AddTeamInProject", projectId, teamTag);

        }

        public async Task AddUserInProject(int projectId, string userTag)
        {

            await HubClient.SendAsync("AddUserInProject", projectId, userTag);
        }

        public async Task CreateProject(int userId, string name)
        {
            await HubClient.SendAsync("CreateProject", userId, name);

        }

        public async Task DeleteProject(int projectId)
        {
            await HubClient.SendAsync("DeleteProject", projectId);

        }


        public async Task DeleteUserFromProject(int projectId, string userTag)
        {

            await HubClient.SendAsync("DeleteUserFromProject", projectId, userTag);

        }



        public async Task<List<ProjectModel>> GetProjectsByUserId(int id)
        {
            var httpReply = await client.CurrentHttpClient.GetAsync($"{client.ConnectionString}/Project/list");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                List<ProjectModel> projects = await httpReply.Content.ReadFromJsonAsync<List<ProjectModel>>();

                return projects;
            }
            throw new ConnectionException();
        }

        public async Task LeaveFromProject(int  projectId, int userId)
        {
            await HubClient.SendAsync("LeaveFromProject", projectId, userId);

        }

        public async Task UpdateProject(ProjectEntity project)
        {
            await HubClient.SendAsync("UpdateProject", project);
        }
    }
}
