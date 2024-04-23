using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using System.Xml.Linq;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.LocalDB.Models;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectRepositoryImplementation(IHttpClient client) : IProjectRepository
    {

        private HubConnection HubClient = ProjectHubConnection.Instance.GetClient();


        public Task AddTeamInProject(int projectId, string teamTag)
        {
            var content = JsonContent.Create(teamTag);

            var httpReply = client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/Project/{projectId}/add-team", content).Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return Task.CompletedTask;
            }
            throw new ConnectionException();
        }

        public async Task AddUserInProject(int projectId, string userTag)
        {

            await HubClient.SendAsync("AddUserInProject", projectId, userTag);
        }

        public async Task<ProjectModel> CreateProject(string name)
        {
            var content = JsonContent.Create(name);

            var httpReply =  client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/Project/create", content).Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return await httpReply.Content.ReadFromJsonAsync<ProjectModel>();
            }
            throw new ConnectionException();
        }

        public async Task DeleteProject(int projectId)
        {
            await HubClient.SendAsync("DeleteProject", projectId);



            //var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/Project/{projectId}/delete");

            //if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{
            //    throw new NotFoundException();
            //}
            //else if (httpReply.IsSuccessStatusCode)
            //{
            //    return;
            //}
            //throw new ConnectionException();
        }


        public async Task DeleteUserFromProject(int projectId, string userTag)
        {

            await HubClient.SendAsync("DeleteUserFromProject", projectId, userTag);


            //var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/Project/{projectId}/delete-user/{userTag}");

            //if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{
            //    throw new NotFoundException();
            //}
            //else if (httpReply.IsSuccessStatusCode)
            //{
            //    return;
            //}
            //throw new ConnectionException();
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
