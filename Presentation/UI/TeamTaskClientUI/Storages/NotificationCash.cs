using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.UI.Storages
{
    public class NotificationCash : INotificationCash
    {
        public ObservableCollection<NotificationModel> Notifications { get; set; }

        public NotificationCash(IMessengerEvents messengerEvents, IProjectsEvents projectsEvents, ITeamsEvents teamsEvents)
        {
            messengerEvents.NotificationAdded += OnNotificationAdded;
            projectsEvents.NotificationAdded += OnNotificationAdded;
            teamsEvents.NotificationAdded += OnNotificationAdded;
        }

        private void OnNotificationAdded(object? sender, NotificationModel e)
        {
            App.Current.Dispatcher.Invoke(() => Notifications.Add(e));
        }
    }
}
