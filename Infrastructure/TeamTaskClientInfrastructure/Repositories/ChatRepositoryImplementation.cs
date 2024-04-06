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
                         .PostAsync($"{client.ConnectionString}/Chat/add-user-in-chat/chat-id={chatId}&user-tag={userTag}", new StringContent(""));

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
              .DeleteAsync($"{client.ConnectionString}/Chat/leave-chat/chat-id={chatId}&user-id={userId}");

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
               .DeleteAsync($"{client.ConnectionString}/Chat/delete-user-from-chat/chat-id={chatId}&user-tag={userTag}");

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
            var httpReply = await client.CurrentHttpClient.GetAsync($"{client.ConnectionString}/Chat/get-chats/user-id={userId}");

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


        public async Task UpdateChat(ChatDTO chatData)
        {
            var httpReply = await client.CurrentHttpClient
                .PatchAsync($"{client.ConnectionString}/Chat/update-chat/chat-id={chatData.ID}&name={chatData.Name}&admin-tag={chatData.AdminTag}", new StringContent(""));

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
               .PostAsync($"{client.ConnectionString}/Chat/create-private-chat/user-id={userId}&second-user-tag={secondUserTag}", new StringContent("create")).Result;

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
                .PostAsync($"{client.ConnectionString}/Chat/create-group-chat/user-id={userId}&group-chat-name={name}", new StringContent(""));

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
