using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents
{
    public interface ITeamsEvents
    {
        public event EventHandler<TeamModel> TeamUpdated;
        public event EventHandler<TeamModel> TeamCreated;
        public event EventHandler<int> TeamDeleted;
        public event EventHandler<UserModel> AddNewUserInTeam;
        public event EventHandler<string> DeleteUserFromTeam;

        public event EventHandler<NotificationModel> NotificationAdded;
    }
}
