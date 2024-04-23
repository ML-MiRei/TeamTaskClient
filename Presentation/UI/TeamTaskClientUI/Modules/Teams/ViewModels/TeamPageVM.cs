using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.AddUserInTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteUserFromTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.LeaveTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.UpdateTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.UI.Modules.Profile.ViewModels;

namespace TeamTaskClient.UI.Modules.Teams.ViewModels
{
    class TeamPageVM : ViewModelBase
    {
        private static IMediator _mediator;


        public event EventHandler InterfaceRefresh;


        public TeamPageVM(IMediator mediator)
        {
            _mediator = mediator;
            Teams = new ObservableCollection<TeamModel>(mediator.Send(new GetTeamsByUserIdCommand { UserId = Properties.Settings.Default.userId }).Result);

            CreateTeam = new NewTeamCommand(this);
            InputSearchString = "Team name..";


            TeamHubClient.TeamUpdated += OnTeamUpdated;
            TeamHubClient.AddNewUserInTeam += OnAddNewUserInTeam;
            TeamHubClient.AddNewTeam += OnAddNewTeam;
            TeamHubClient.TeamDeleted += OnTeamDeleted;
            TeamHubClient.DeleteUserFromTeam += OnDeleteUserFromTeam;
            TeamHubClient.TeamCreated += OnTeamCreated;

        }

        private void OnTeamCreated(object? sender, TeamModel e)
        {
            App.Current.Dispatcher.Invoke(() => _teams.Add(e));
        }

        private void OnDeleteUserFromTeam(object? sender, string e)
        {
            App.Current.Dispatcher.Invoke(() => _teams.First(t => t.TeamId == (int)sender)
            .Users
            .Remove(_teams.First(t => t.TeamId == (int)sender).Users.First(u => u.UserTag == e)));
            InterfaceRefresh?.Invoke(null, new EventArgs());

        }

        private void OnTeamDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => _teams.Remove(_teams.First(t => t.TeamId == e)));
        }

        private void OnAddNewTeam(object? sender, TeamModel e)
        {
            App.Current.Dispatcher.Invoke(() => _teams.Add(e));
        }

        private void OnAddNewUserInTeam(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _teams.First(t => t.TeamId == (int)sender).Users.Add(e);
            });
            InterfaceRefresh?.Invoke(null, new EventArgs());
        }

        private void OnTeamUpdated(object? sender, TeamModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _teams.First(t => t.TeamId == e.TeamId).TeamLeadName = e.TeamLeadName;
                _teams.First(t => t.TeamId == e.TeamId).TeamName = e.TeamName;
                _teams.First(t => t.TeamId == e.TeamId).UserRole = e.UserRole;

            });

            OnPropertyChanged(nameof(Teams));
        }


        private ObservableCollection<TeamModel> _teams;
        public ObservableCollection<TeamModel> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }



        private static string _inputSearchString;
        public string InputSearchString
        {
            get { return _inputSearchString; }
            set
            {
                _inputSearchString = value;
                OnPropertyChanged(nameof(InputSearchString));
            }
        }




        public async void SettingsTeam(TeamModel teamModel)
        {
            if (teamModel.UserRole == (int)UserRole.EMPLOYEE)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Leave" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");
                        if (alertDialogWindow.ShowDialog().Value)
                        {
                            await _mediator.Send(new LeaveTeamCommand { TeamId = teamModel.TeamId, UserId = Properties.Settings.Default.userId });
                        }
                    }
                    catch (Exception)
                    {
                        ErrorWindow.Show("Error leave team");
                    }
                }
            }
            else
            {

                var listActions = new List<string> { "Add user", "Change name", "Change lead" };

                if (teamModel.Users.Count > 1)
                    listActions.Add("Delete user from team");

                if (Teams.First(t => t.TeamId == teamModel.TeamId).Users.Count == 1)
                {
                    listActions.Add("Leave");
                }

                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", listActions);

                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    switch (SelectActionsDialogVM.SelectedAction)
                    {
                        case "Add user":

                            InputDialogWindow updatePropertiesDialogWindow = new InputDialogWindow("User tag", "Add", new List<string> { "" });
                            if (updatePropertiesDialogWindow.ShowDialog().Value)
                            {
                                var userTag = updatePropertiesDialogWindow.GetInputValue()[0];

                                if (userTag != Properties.Settings.Default.userTag)
                                {
                                    _mediator.Send(new AddUserInTeamCommand { TeamId = teamModel.TeamId, UserTag = userTag });
                                }
                                else
                                {
                                    ErrorWindow.Show("You can't add \nyourself to the team\r\n");
                                }

                            }


                            break;

                        case "Change name":
                            try
                            {
                                InputDialogWindow inputPropertiesDialogWindow = new InputDialogWindow("Change team name", "Save", new List<string> { teamModel.TeamName });

                                if (inputPropertiesDialogWindow.ShowDialog().Value)
                                {
                                  await _mediator.Send(new UpdateTeamCommand { TeamId = teamModel.TeamId, Name = inputPropertiesDialogWindow.GetInputValue()[0], LeaderTag = Properties.Settings.Default.userTag });

                                }
                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error change name team");
                            }
                            break;

                        case "Change lead":

                            try
                            {
                                SelectActionsDialogWindow selectLead =
                               new SelectActionsDialogWindow("Select user", Teams.First(t => t.TeamId == teamModel.TeamId).Users
                                                                                 .Where(u => u.UserTag != Properties.Settings.Default.userTag)
                                                                                 .Select(u => u.FirstName + ", tag: " + u.UserTag)
                                                                                 .ToList());

                                if (selectLead.ShowDialog().Value)
                                {
                                    var userTag = selectLead.GetSelectedAction().Substring(selectLead.GetSelectedAction().IndexOf(": ") + 2);

                                   await _mediator.Send(new UpdateTeamCommand { TeamId = teamModel.TeamId, LeaderTag = userTag });

                                }

                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error change team lead");
                            }
                            break;

                        case "Delete user from team":

                            SelectActionsDialogWindow selectActions =
                                new SelectActionsDialogWindow("Select user", Teams.First(t => t.TeamId == teamModel.TeamId).Users
                                                                                  .Where(u => u.UserTag != Properties.Settings.Default.userTag)
                                                                                  .Select(u => u.FirstName + ", tag: " + u.UserTag)
                                                                                  .ToList());

                            if (selectActions.ShowDialog().Value)
                            {
                                var userTag = selectActions.GetSelectedAction().Substring(selectActions.GetSelectedAction().IndexOf(": ") + 2);

                                await _mediator.Send(new DeleteUserFromTeamCommand { TeamId = teamModel.TeamId, Tag = userTag });

                                var deletedUser = Teams.First(c => c.TeamId == teamModel.TeamId).Users.First(u => u.UserTag == userTag);

                            }


                            break;

                        case "Leave":

                            try
                            {
                                AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");
                                if (alertDialogWindow.ShowDialog().Value)
                                {
                                    _mediator.Send(new LeaveTeamCommand { TeamId = teamModel.TeamId, UserId = Properties.Settings.Default.userId });
                                }
                            }
                            catch (Exception)
                            {
                                ErrorWindow.Show("Error leave team");
                            }

                            break;
                    }
                }

            }
        }



        public ICommand CreateTeam { get; }




        private class NewTeamCommand(TeamPageVM vM) : CommandBase
        {
            public async override void Execute(object? parameter)
            {
                CreateSubjectDialogWindow dialogWindow = new CreateSubjectDialogWindow("Creating a team", new List<string> { "Team name:" });
                if (dialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        await _mediator.Send(new CreateTeamCommand { Name = (dialogWindow.GetCreatingProperties()[0]), UserId = Properties.Settings.Default.userId });
                    }
                    catch
                    {
                        ErrorWindow.Show("Error creating the command");
                    }

                }
            }
        }

    }
}
