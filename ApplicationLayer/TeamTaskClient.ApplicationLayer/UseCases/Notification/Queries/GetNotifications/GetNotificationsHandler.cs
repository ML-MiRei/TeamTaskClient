using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Notification.Queries.GetNotifications
{
    public class GetNotificationsHandler(INotificationRepository notificationRepository) : IRequestHandler<GetNotificationsQuery, List<NotificationModel>>
    {
        public Task<List<NotificationModel>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var notifications = notificationRepository.GetNotificationsByUserId(request.UserId);
                if (notifications == null || notifications.Result.Count == 0)
                {
                    throw new NotFoundException();
                }
                return notifications;
            }
            catch (Exception)
            {
                return Task.FromResult(new List<NotificationModel>());
            }
        }
    }
}
