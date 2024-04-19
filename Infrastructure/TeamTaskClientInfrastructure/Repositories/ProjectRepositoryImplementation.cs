using System.Net.Http.Json;
using System.Xml.Linq;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectRepositoryImplementation(IHttpClient client) : IProjectRepository
    {
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

        public Task AddUserInProject(int projectId, string userTag)
        {
            var content = JsonContent.Create(userTag);

            var httpReply = client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/Project/{projectId}/add-user", content).Result;

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
            var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/Project/{projectId}/delete");

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


        public async Task DeleteUserFromProject(int projectId, string userTag)
        {
            var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/Project/{projectId}/delete-user/{userTag}");

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

        public async Task LeaveFromProject(int projectId)
        {
            var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/Project/{projectId}/leave");

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

        public async Task UpdateProject(ProjectEntity project)
        {

            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/Project/{project.ID}/update", JsonContent.Create(project));

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
