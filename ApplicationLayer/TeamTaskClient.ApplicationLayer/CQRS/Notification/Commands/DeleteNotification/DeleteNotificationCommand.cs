using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Notification.Commands.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest
    {
        public int NotificationId { get; set; }
    }
}
