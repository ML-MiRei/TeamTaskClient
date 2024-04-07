using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Notification.Queries.GetNotifications
{
    public class GetNotificationsQuery : IRequest<List<NotificationModel>>
    {
        public int UserId {  get; set; }
    }
}
