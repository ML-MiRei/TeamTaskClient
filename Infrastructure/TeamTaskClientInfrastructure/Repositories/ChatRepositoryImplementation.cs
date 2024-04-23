using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.LocalDB.Models;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ChatRepositoryImplementation(IHttpClient client) : IChatRepository
    {

        private HubConnection HubClient = ChatHubConnection.Instance.GetClient();

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


        public async void CreatePrivateChat(int userId, string secondUserTag)
        {

            await HubClient.SendAsync("CreatePrivateChat", userId, secondUserTag);

        }


        public async Task<ChatModel> CreateGroupChat(int userId, string name)
        {
            var content = JsonContent.Create(name);

            var httpReply = client.CurrentHttpClient
                .PostAsync($"{client.ConnectionString}/Chat/group", content).Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var chat = await httpReply.Content.ReadFromJsonAsync<ChatModel>();
                return chat;
            }
            throw new ConnectionException();
        }
    }
}
