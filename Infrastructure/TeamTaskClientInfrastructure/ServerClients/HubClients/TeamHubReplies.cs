using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class TeamHubReplies : ITeamsEvents
    {
        public HubConnection _hubConnection;

        public TeamHubReplies(ITeamHubConnection teamHubConnection, int userId, string userTag)
        {
            _hubConnection = teamHubConnection.HubConnection;


            _hubConnection.SendAsync("ConnectUserWithTeams", userId, userTag).Wait();


            _hubConnection.On<string, TeamModel>("TeamUpdatedReply", (teamLeadTag, teamModel) =>
            {
                TeamUpdated?.Invoke(teamLeadTag, teamModel);
            });


            _hubConnection.On<int, UserModel>("AddUserInTeamReply", (teamId, user) =>
            {
                AddNewUserInTeam?.Invoke(teamId, user);
            });


            _hubConnection.On<int, string>("DeleteUserFromTeamReply", (teamId, userTag) =>
            {
                DeleteUserFromTeam?.Invoke(teamId, userTag);
            });


            _hubConnection.On<TeamModel>("NewTeamReply", (teamModel) =>
            {
                TeamCreated?.Invoke(null, teamModel);
            });


            _hubConnection.On<int>("DeleteTeamReply", (teamId) =>
            {
                TeamDeleted?.Invoke(null, teamId);
            });


            _hubConnection.On<NotificationModel>("NotificationReply", (notificationModel) =>
            {
                NotificationAdded?.Invoke(null, notificationModel);
            });

        }


        public event EventHandler<TeamModel> TeamUpdated;
        public event EventHandler<TeamModel> TeamCreated;
        public event EventHandler<int> TeamDeleted;
        public event EventHandler<UserModel> AddNewUserInTeam;
        public event EventHandler<string> DeleteUserFromTeam;

        public event EventHandler<NotificationModel> NotificationAdded;

    }
}
