using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.AddUserInTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteUserFromTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.LeaveTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.UpdateTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Modules.Teams.ViewModels
{
    class TeamPageVM : ViewModelBase
    {
        private static IMediator _mediator;

        public TeamPageVM(IMediator mediator)
        {
            _mediator = mediator;
            Teams = new ObservableCollection<TeamModel>(mediator.Send(new GetTeamsByUserIdCommand { UserId = Properties.Settings.Default.userId }).Result);

            CreateTeam = new NewTeamCommand(this);
            InputSearchString = "Team name..";

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




        public void SettingsTeam(TeamModel teamModel)
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
                            _mediator.Send(new LeaveTeamCommand { TeamId = teamModel.TeamId, UserId = Properties.Settings.Default.userId });
                            var deletedTeam = Teams.First(t => t.TeamId == teamModel.TeamId);
                            Teams.Remove(deletedTeam);
                            OnPropertyChanged(nameof(Teams));
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

                var listActions = new List<string> { "Add user", "Delete user from team", "Change name", "Change lead" };
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
                                _mediator.Send(new AddUserInTeamCommand { TeamId = teamModel.TeamId, UserTag = userTag });

                                var newUser = _mediator.Send(new GetUserByTagQuery { UserTag = userTag }).Result;

                                Teams.First(c => c.TeamId == teamModel.TeamId).Users.Add(new UserModel
                                {
                                    Email = newUser.Email,
                                    UserTag = userTag,
                                    SecondName = newUser.SecondName,
                                    FirstName = newUser.FirstName,
                                    LastName = newUser.LastName,
                                    PhoneNumber = newUser.PhoneNumber
                                });
                                OnPropertyChanged(nameof(Teams));

                            }


                            break;

                        case "Change name":
                            try
                            {
                                InputDialogWindow inputPropertiesDialogWindow = new InputDialogWindow("Change team name", "Save", new List<string> { teamModel.TeamName });

                                if (inputPropertiesDialogWindow.ShowDialog().Value)
                                {
                                    _mediator.Send(new UpdateTeamCommand { TeamId = teamModel.TeamId, Name = inputPropertiesDialogWindow.GetInputValue()[0] });
                                    Teams.First(t => t.TeamId == teamModel.TeamId).TeamName = inputPropertiesDialogWindow.GetInputValue()[0];
                                    OnPropertyChanged(nameof(Teams));

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
                                                                                 .Where(u=> u.UserTag != Properties.Settings.Default.userTag)
                                                                                 .Select(u => u.FirstName + ", tag: " + u.UserTag)
                                                                                 .ToList());

                                if (selectLead.ShowDialog().Value)
                                {
                                    var userTag = selectLead.GetSelectedAction().Substring(selectLead.GetSelectedAction().IndexOf(": ") + 2);

                                    _mediator.Send(new UpdateTeamCommand { TeamId = teamModel.TeamId, LeaderTag = userTag });

                                    Teams.First(c => c.TeamId == teamModel.TeamId).UserRole = (int)UserRole.EMPLOYEE;
                                    OnPropertyChanged(nameof(Teams));

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

                                _mediator.Send(new DeleteUserFromTeamCommand { TeamId = teamModel.TeamId, Tag = userTag });

                                var deletedUser = Teams.First(c => c.TeamId == teamModel.TeamId).Users.First(u => u.UserTag == userTag);

                                Teams.First(c => c.TeamId == teamModel.TeamId).Users.Remove(deletedUser);
                                OnPropertyChanged(nameof(Teams));

                            }


                            break;

                        case "Leave":

                            try
                            {
                                AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");
                                if (alertDialogWindow.ShowDialog().Value)
                                {
                                    _mediator.Send(new LeaveTeamCommand { TeamId = teamModel.TeamId, UserId = Properties.Settings.Default.userId });
                                    var deletedTeam = Teams.First(t => t.TeamId == teamModel.TeamId);
                                    Teams.Remove(deletedTeam);
                                    OnPropertyChanged(nameof(Teams));
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
            public override void Execute(object? parameter)
            {
                CreateSubjectDialogWindow dialogWindow = new CreateSubjectDialogWindow("Creating a team", new List<string> { "Team name:" });
                if (dialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        var team = _mediator.Send(new CreateTeamCommand { Name = (dialogWindow.GetCreatingProperties()[0]), UserId = Properties.Settings.Default.userId }).Result;

                        vM.Teams.Add(team);
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
