using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task AddUserGroupChatByTag(string tag, int idChat);
        Task DeleteUserFromChatByTag(string tag, int idChat);
        Task<ChatModel> CreatePrivateChat(int userId, string secondUserTag);
        Task<ChatModel> CreateGroupChat(int userId, string name);
        Task UpdateChat(ChatEntity chatEntity);
        Task LeaveChat(int userId, int chatId);
        Task<List<ChatModel>> GetChatByIdUser(int userId);

    }
}
