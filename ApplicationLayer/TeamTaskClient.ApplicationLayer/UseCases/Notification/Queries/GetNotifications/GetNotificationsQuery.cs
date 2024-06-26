﻿using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Notification.Queries.GetNotifications
{
    public class GetNotificationsQuery : IRequest<List<NotificationModel>>
    {
        public int UserId { get; set; }
    }
}
