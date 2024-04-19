using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Task DeleteMessage(int chatId, int messageId);
        Task UpdateMessage(MessageEntity message);
        void CreateMessage(MessageDTO message);

    }
}
