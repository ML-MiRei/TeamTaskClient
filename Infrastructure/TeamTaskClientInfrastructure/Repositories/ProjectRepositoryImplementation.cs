using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.LocalDB.Models;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectRepositoryImplementation(IHttpClient client) : IProjectRepository
    {
        public async Task<ProjectModel> CreateProject(string name)
        {
            var httpReply = await client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/Project/create", new StringContent(name));

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
                return await httpReply.Content.ReadFromJsonAsync<List<ProjectModel>>();
            }
            throw new ConnectionException();
        }


        public async Task UpdateProject(ProjectEntity project)
        {
            var httpReply = await client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/Project/{project.ID}/update", JsonContent.Create(project));

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
