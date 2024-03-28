using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Notification.Commands.DeleteNotification
{
    internal class DeleteNotificationHandler(INotificationRepository notificationRepository) : IRequestHandler<DeleteNotificationCommand>
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
