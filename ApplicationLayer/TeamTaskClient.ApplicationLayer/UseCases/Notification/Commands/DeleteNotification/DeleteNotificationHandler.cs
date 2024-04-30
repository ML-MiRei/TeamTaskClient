using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Notification.Commands.DeleteNotification
{
    public class DeleteNotificationHandler(INotificationRepository notificationRepository) : IRequestHandler<DeleteNotificationCommand>
    {
        public Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                notificationRepository.DeleteNotification(request.NotificationId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
