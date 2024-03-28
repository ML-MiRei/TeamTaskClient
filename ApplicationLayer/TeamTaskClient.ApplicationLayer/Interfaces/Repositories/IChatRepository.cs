using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<UserEntity> AddUserGroupChatByTag(string tag, int idChat);
        Task DeleteUserGroupChatByTag(string tag, int idChat);
        Task<ChatEntity> CreateChat(ChatDTO chatEntity);
        Task UpdateChat(ChatDTO chatEntity);
        Task DeleteChat(int userId, int chatId);
        Task<List<ChatEntity>> GetChatByIdUser(int userId);
        Task<UserEntity> GetGroupChatAdmin(int chatId);

    }
}
