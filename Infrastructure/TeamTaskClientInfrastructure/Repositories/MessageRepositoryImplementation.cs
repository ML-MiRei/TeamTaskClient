using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.LocalDB.Context;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class MessageRepositoryImplementation : IMessageRepository
    {
        public Task<MessageEntity> CreateMessage(MessageDTO message)
        {
            throw new NotImplementedException();

        }

        public Task DeleteMessage(int chatId, int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MessageEntity>> GetMessagesByChatId(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessage(MessageDTO message)
        {
            throw new NotImplementedException();
        }
    }
}
