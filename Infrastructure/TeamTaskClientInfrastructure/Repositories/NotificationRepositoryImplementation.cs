using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class NotificationRepositoryImplementation : INotificationRepository
    {
        public Task DeleteNotification(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<NotificationEntity>> GetNotificationsByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
