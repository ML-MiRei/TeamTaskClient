using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class TeamRepositoryImplementation(IHttpClient httpClient) : ITeamRepository
    {
        public async Task AddUserInTeamByTag(int teamId, string userTag)
        {
            var httpReply = await httpClient.CurrentHttpClient.PostAsync($"{httpClient.ConnectionString}/Team/{teamId}/add-user", JsonContent.Create(userTag));

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

        public async Task<TeamModel> CreateTeam(TeamEntity teamData)
        {

            var httpReply = httpClient.CurrentHttpClient
                                .PostAsync($"{httpClient.ConnectionString}/Team/create", JsonContent.Create(teamData.Name)).Result;


            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return await httpReply.Content.ReadFromJsonAsync<TeamModel>();
            }
            throw new ConnectionException();
        }

        public async Task DeleteUserFromTeam(string userTag, int teamId)
        {
            var httpReply = await httpClient.CurrentHttpClient
                                .DeleteAsync($"{httpClient.ConnectionString}/Team/{teamId}/delete-user/{userTag}");

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


        public async Task<List<TeamModel>> GetTeamsByUserId(int userId)
        {
            var httpReply = await httpClient.CurrentHttpClient
                      .GetAsync($"{httpClient.ConnectionString}/Team/list");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return await httpReply.Content.ReadFromJsonAsync<List<TeamModel>>();
            }
            throw new ConnectionException();
        }

        public async Task LeaveTeam(int userId, int teamId)
        {
            var httpReply = await httpClient.CurrentHttpClient
                                            .DeleteAsync($"{httpClient.ConnectionString}/Team/{teamId}/leave");

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


        public async Task UpdateTeam(TeamEntity teamData)
        {
            var httpReply = await httpClient.CurrentHttpClient
                    .PatchAsync($"{httpClient.ConnectionString}/Team/{teamData.ID}/update", JsonContent.Create(teamData));

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
