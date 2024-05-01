using System.Collections.ObjectModel;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.UI.Storages
{
    public class TeamsCash : ITeamsCash
    {
        public ObservableCollection<TeamModel> Teams { get => _teams; set => _teams = value; }
        private static ObservableCollection<TeamModel> _teams = new ObservableCollection<TeamModel>();

        public event EventHandler TeamUpdated;


        public TeamsCash(ITeamsEvents teamsEvents)
        {
            teamsEvents.TeamUpdated += OnTeamUpdated;
            teamsEvents.AddNewUserInTeam += OnAddNewUserInTeam;
            teamsEvents.TeamDeleted += OnTeamDeleted;
            teamsEvents.DeleteUserFromTeam += OnDeleteUserFromTeam;
            teamsEvents.TeamCreated += OnTeamCreated;
        }



        private void OnTeamCreated(object? sender, TeamModel e)
        {
           App.Current.Dispatcher.Invoke(() => Teams.Add(e));
        }

        private void OnDeleteUserFromTeam(object? sender, string e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _teams.First(t => t.TeamId == (int)sender)
                        .Users.Remove(_teams.First(t => t.TeamId == (int)sender).Users.First(u => u.UserTag == e));
                TeamUpdated?.Invoke(e, EventArgs.Empty);
            });
        }

        private void OnTeamDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => Teams.Remove(_teams.First(t => t.TeamId == e)));
        }


        private void OnAddNewUserInTeam(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _teams.First(t => t.TeamId == (int)sender).Users.Add(e);
                TeamUpdated?.Invoke(e, EventArgs.Empty);

            });
        }

        private void OnTeamUpdated(object? sender, TeamModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _teams.First(t => t.TeamId == e.TeamId).TeamLeadName = e.TeamLeadName;
                _teams.First(t => t.TeamId == e.TeamId).TeamName = e.TeamName;
                _teams.First(t => t.TeamId == e.TeamId).UserRole = (string)sender == Properties.Settings.Default.userTag ? (int)UserRoleEnum.LEAD : (int)UserRoleEnum.EMPLOYEE;
                TeamUpdated?.Invoke(e, EventArgs.Empty);

            });
        }



    }
}
