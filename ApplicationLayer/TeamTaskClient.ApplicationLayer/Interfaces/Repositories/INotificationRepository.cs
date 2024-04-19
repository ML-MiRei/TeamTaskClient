using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<List<NotificationModel>> GetNotificationsByUserId(int id);
        Task DeleteNotification(int id);
    }
}
