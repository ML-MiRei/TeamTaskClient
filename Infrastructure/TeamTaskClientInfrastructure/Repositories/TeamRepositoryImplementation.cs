using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public class TeamRepositoryImplementation(IHttpClient httpClient) : ITeamRepository
    {
        public async Task AddUserInTeamByTag(string teamTag, string userTag)
        {
            var httpReply = await httpClient.CurrentHttpClient.PostAsync($"{httpClient.ConnectionString}/Team/add-user/user-tag={userTag}&team-tag={teamTag}", new StringContent(""));

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

        public async Task<TeamModel> CreateTeam(TeamDTO teamData)
        {

            var httpReply = await httpClient.CurrentHttpClient
                                .PostAsync($"{httpClient.ConnectionString}/Team/create/user-tag={teamData.LeadTag}&team-name={teamData.Name}", new StringContent(""));

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var team =  await httpReply.Content.ReadFromJsonAsync<TeamModel>();
                return team;
            }
            throw new ConnectionException();
        }

        public async Task DeleteUserFromTeam(string userTag, string teamTag)
        {
            var httpReply = await httpClient.CurrentHttpClient
                                .DeleteAsync($"{httpClient.ConnectionString}/Team/delete-user-from-team/user-tag={userTag}&team-tag={teamTag}");

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
                      .GetAsync($"{httpClient.ConnectionString}/Team/list/user-id={userId}");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var teams = await httpReply.Content.ReadFromJsonAsync<List<TeamModel>>();
                return teams;
            }
            throw new ConnectionException();
        }

        public async Task LeaveTeam(int userId, int teamId)
        {
            var httpReply = await httpClient.CurrentHttpClient
                                            .DeleteAsync($"{httpClient.ConnectionString}/Team/leave-from-team/user-id={userId}&team-id={teamId}");

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



        // attention: TeamDTO -- TeamModel: prokatit - horosho
        public async Task UpdateTeam(TeamDTO teamData)
        {
            var httpReply = await httpClient.CurrentHttpClient
                    .PatchAsync($"{httpClient.ConnectionString}/Team/update/team={teamData}",  JsonContent.Create(teamData));

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
