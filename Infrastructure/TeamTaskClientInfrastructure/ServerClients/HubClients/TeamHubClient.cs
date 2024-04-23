using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.ServerClients.Connections;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class TeamHubClient
    {
        public HubConnection HubClient;


        private static TeamHubClient _instance;
        public static TeamHubClient GetInstance(int userId, string userTag)
        {

            if (_instance == null)
                _instance = new TeamHubClient(userId, userTag);
            return _instance;

        }
        public static TeamHubClient GetInstance()
        {
            return _instance;
        }


        private TeamHubClient(int userId, string userTag)
        {
            TeamHubConnection chatHubClient = TeamHubConnection.Instance;

            HubClient = chatHubClient.GetClient();

            HubClient.SendAsync("ConnectUserWithTeams", userId, userTag).Wait();


            HubClient.On<TeamModel>("TeamUpdatedReply", (teamModel) =>
            {
                TeamUpdated?.Invoke(null, teamModel);
            });


            HubClient.On<int, UserModel>("AddUserInTeamReply", (teamId, user) =>
            {
                AddNewUserInTeam?.Invoke(teamId, user);
            });
            


            HubClient.On<TeamModel>("CreateTeamReply", (teamModel) =>
            {
                TeamCreated?.Invoke(null, teamModel);
            });


            HubClient.On<int, string>("DeleteUserFromTeamReply", (teamId, userTag) =>
            {
                DeleteUserFromTeam?.Invoke(teamId, userTag);
            });


            HubClient.On<TeamModel>("NewTeamReply", (teamModel) =>
            {
                AddNewTeam?.Invoke(null, teamModel);
            });
            

            HubClient.On<int>("DeleteTeamReply", (teamId) =>
            {
                TeamDeleted?.Invoke(null, teamId);
            });



        }


        public static event EventHandler<TeamModel> TeamUpdated;
        public static event EventHandler<TeamModel> TeamCreated;
        public static event EventHandler<int> TeamDeleted;
        public static event EventHandler<TeamModel> AddNewTeam;
        public static event EventHandler<UserModel> AddNewUserInTeam;
        public static event EventHandler<string> DeleteUserFromTeam;

    }
}
