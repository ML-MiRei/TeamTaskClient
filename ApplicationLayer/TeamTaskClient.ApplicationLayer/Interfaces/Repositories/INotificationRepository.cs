using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<List<NotificationModel>> GetNotificationsByUserId(int id);
        Task DeleteNotification(int id);
    }
}
