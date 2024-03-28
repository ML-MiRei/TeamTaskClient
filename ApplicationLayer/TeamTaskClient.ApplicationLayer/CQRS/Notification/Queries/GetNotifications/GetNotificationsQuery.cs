using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Notification.Queries.GetNotifications
{
    internal class GetNotificationsQuery : IRequest<List<NotificationEntity>>
    {
        public int UserId {  get; set; }
    }
}
