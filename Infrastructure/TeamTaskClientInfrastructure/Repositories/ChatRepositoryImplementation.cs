using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.LocalDB.Models;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ChatRepositoryImplementation(IHttpClient client) : IChatRepository
    {
        public async Task AddUserGroupChatByTag(string userTag, int chatId)
        {
            var httpReply = await client.CurrentHttpClient
                         .PostAsync($"{client.ConnectionString}/Chat/{chatId}/add-user", new StringContent(userTag));

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

            var httpReply = await client.CurrentHttpClient
                .PatchAsync($"{client.ConnectionString}/Chat/update", JsonContent.Create(chatData));

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
            var httpReply =  client.CurrentHttpClient
               .PostAsync($"{client.ConnectionString}/Chat/private", new StringContent(secondUserTag)).Result;

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
            var httpReply = await client.CurrentHttpClient
                .PostAsync($"{client.ConnectionString}/Chat/group", new StringContent(name));

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
