using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ChatRepositoryImplementation(IHttpClient client) : IChatRepository
    {
        public async Task AddUserGroupChatByTag(string userTag, int chatId)
        {
            var content = JsonContent.Create(userTag);

            var httpReply = await client.CurrentHttpClient
                         .PostAsync($"{client.ConnectionString}/Chat/{chatId}/add-user", content);

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


        public async Task LeaveChat(int userId, int chatId)
        {
            var httpReply = await client.CurrentHttpClient
              .DeleteAsync($"{client.ConnectionString}/Chat/{chatId}/leave");

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

        public async Task DeleteUserFromChatByTag(string userTag, int chatId)
        {
            var httpReply = await client.CurrentHttpClient
               .DeleteAsync($"{client.ConnectionString}/Chat/{chatId}/delete-user/{userTag}");

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
            var content = JsonContent.Create(chatData);

            var httpReply = await client.CurrentHttpClient
                .PatchAsync($"{client.ConnectionString}/Chat/update", content);

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

        public async Task<ChatModel> CreatePrivateChat(int userId, string secondUserTag)
        {

            var content = JsonContent.Create(secondUserTag);

            var httpReply =  client.CurrentHttpClient
               .PostAsync($"{client.ConnectionString}/Chat/private", content).Result;

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
