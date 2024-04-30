using Microsoft.AspNetCore.SignalR.Client;
using System.Net;
using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class TeamRepositoryImplementation(IHttpClient httpClient) : ITeamRepository
    {
        private HubConnection HubClient = TeamHubConnection.Instance.GetClient();



        public async Task AddUserInTeamByTag(int teamId, string userTag)
        {
            await HubClient.SendAsync("AddUser", teamId, userTag);

        }

        public async Task CreateTeam(TeamEntity teamData)
        {
            await HubClient.SendAsync("CreateTeam", teamData.TeamLeadId, teamData.Name);

        }

        public async Task DeleteUserFromTeam(string userTag, int teamId)
        {

            await HubClient.SendAsync("DeleteUser", teamId, userTag);

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

            await HubClient.SendAsync("LeaveFromTeam", userId, teamId);


        }


        public async Task UpdateTeam(TeamEntity teamData)
        {

            await HubClient.SendAsync("Update", teamData);

        }
    }
}
