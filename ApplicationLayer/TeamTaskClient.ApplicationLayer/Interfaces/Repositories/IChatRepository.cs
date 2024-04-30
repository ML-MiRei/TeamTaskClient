using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task AddUserGroupChatByTag(string tag, int idChat);
        Task DeleteUserFromChatByTag(string tag, int idChat);
        Task CreatePrivateChat(int userId, string secondUserTag);
        Task CreateGroupChatByTeam(int userId, int teamId);
        Task CreateGroupChatByProject(int userId, int projectId);
        Task CreateGroupChat(int userId, string name);
        Task UpdateChat(ChatEntity chatEntity);
        Task LeaveChat(int userId, int chatId);
        Task<List<ChatModel>> GetChatByIdUser(int userId);

    }
}
