using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Cash
{
    public interface INotificationCash
    {
        public ObservableCollection<NotificationModel> Notifications { get; set; }
    }
}
