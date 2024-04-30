using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Notification.Commands.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest
    {
        public int NotificationId { get; set; }
    }
}
