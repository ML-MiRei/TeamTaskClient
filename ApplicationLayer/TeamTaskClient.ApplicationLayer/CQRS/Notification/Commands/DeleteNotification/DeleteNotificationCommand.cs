using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Notification.Commands.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest
    {
        public int NotificationId {  get; set; }
    }
}
