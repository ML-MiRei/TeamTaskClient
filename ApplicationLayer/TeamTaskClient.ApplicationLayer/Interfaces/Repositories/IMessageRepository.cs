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
    public interface IMessageRepository
    {
        Task DeleteMessage(int chatId, int messageId);
        Task UpdateMessage(MessageDTO message);
        void CreateMessage(MessageDTO message);

    }
}
