using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ChatRepositoryImplementation(IHttpClient client) : IChatRepository
    {
        public Task<UserEntity> AddUserGroupChatByTag(string tag, int idChat)
        {
            throw new NotImplementedException();
        }

        public Task<ChatEntity> CreateChat(ChatDTO chatEntity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChat(int userId, int chatId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserGroupChatByTag(string tag, int idChat)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatEntity>> GetChatByIdUser(int userId)
        {
            return new List<ChatEntity>() { new ChatEntity() { ID=0, Name="kek"} };
        }

        public Task<UserEntity> GetGroupChatAdmin(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateChat(ChatDTO chatEntity)
        {
            throw new NotImplementedException();
        }
    }
}
