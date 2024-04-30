using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using System.Xml.Linq;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ChatRepositoryImplementation(IHttpClient client, IChatHubConnection chatHubClient) : IChatRepository
    {

        private HubConnection HubClient = chatHubClient.HubConnection;

        public async Task AddUserGroupChatByTag(string userTag, int chatId)
        {

            await HubClient.SendAsync("AddUserInChat", chatId, userTag);

        }


        public async Task LeaveChat(int userId, int chatId)
        {

            await HubClient.SendAsync("LeaveChat", chatId, userId);

        }

        public async Task DeleteUserFromChatByTag(string userTag, int chatId)
        {

            await HubClient.SendAsync("DeleteUserFromChat", chatId, userTag);

        }

        public async Task<List<ChatModel>> GetChatByIdUser(int userId)
        {
            var httpReply = await client.CurrentHttpClient.GetAsync($"{client.ConnectionString}/Chat/list");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return await httpReply.Content.ReadFromJsonAsync<List<ChatModel>>();
            }
            throw new ConnectionException();

        }


        public async Task UpdateChat(ChatEntity chatData)
        {
            await HubClient.SendAsync("UpdateChat", chatData);

        }


        public async Task CreatePrivateChat(int userId, string secondUserTag)
        {

            await HubClient.SendAsync("CreatePrivateChat", userId, secondUserTag);

        }


        public async Task CreateGroupChat(int userId, string name)
        {
            await HubClient.SendAsync("CreateGroupChat", userId, name);

        }

        public async Task CreateGroupChatByTeam(int userId, int teamId)
        {
            await HubClient.SendAsync("CreateGroupChatByTeam", userId, teamId);
        }

        public async Task CreateGroupChatByProject(int userId, int projectId)
        {
            await HubClient.SendAsync("CreateGroupChatByProject", userId, projectId);
        }
    }
}
