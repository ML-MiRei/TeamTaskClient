using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Notification.Queries.GetNotifications
{
    public class GetNotificationsHandler(INotificationRepository notificationRepository) : IRequestHandler<GetNotificationsQuery, List<NotificationEntity>>
    {
        public Task<List<NotificationEntity>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
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
                throw new NotFoundException();
            }
        }
    }
}
