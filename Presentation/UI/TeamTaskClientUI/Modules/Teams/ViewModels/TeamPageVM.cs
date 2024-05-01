using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChat;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.AddUserInTeam;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.CreateTeam;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.DeleteUserFromTeam;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.LeaveTeam;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.UpdateTeam;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Queries.GetTeamsByUserId;
using TeamTaskClient.ApplicationLayer.UseCases.User.Queries.GetUserByTag;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Modules.Teams.ViewModels
{
    class TeamPageVM : ViewModelBase
    {
        private static IMediator _mediator;
        private static ITeamsCash _teamsCash;

        public static TeamPageVM Instance;

        public TeamPageVM(IMediator mediator, ITeamsCash teamsCash)
        {
            _mediator = mediator;
            _teamsCash = teamsCash;

            Instance = this;

            teamsCash.Teams = new ObservableCollection<TeamModel>(mediator.Send(new GetTeamsByUserIdCommand { UserId = Properties.Settings.Default.userId }).Result);
            teamsCash.TeamUpdated += OnTeamUpdated;

            CreateTeam = new NewTeamCommand(this);
            InputSearchString = "Team name..";

        }

        private void OnTeamUpdated(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Teams));
        }

        public ObservableCollection<TeamModel> Teams
        {
            get { return _teamsCash.Teams; }
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
            if (teamModel.UserRole == (int)UserRoleEnum.EMPLOYEE)
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
                var listActions = new List<string> { "Add user", "Change name" };

                if (teamModel.Users.Count > 1)
                {
                    listActions.Add("Delete user from team");
                    listActions.Add("Change lead");
                    listActions.Add("Create chat");
                }
                else
                    listActions.Add("Leave");


                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", listActions);

                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    switch (SelectActionsDialogVM.SelectedAction)
                    {
                        case "Add user":

                            CreateSubjectDialogWindow createSubjectDialogWindow = new CreateSubjectDialogWindow("Find user", new List<string> { "Tag: " });
                            if (createSubjectDialogWindow.ShowDialog().Value)
                            {
                                var userTag = createSubjectDialogWindow.GetCreatingProperties()[0];

                                if (userTag == Properties.Settings.Default.userTag)
                                {
                                    ErrorWindow.Show("You can't add \nyourself to the team");
                                }
                                else
                                {
                                    if (teamModel.Users.Select(u => u.UserTag).Any(t => t == userTag))
                                    {
                                        ErrorWindow.Show("This user is already\n a member of the team");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var user = _mediator.Send(new GetUserByTagQuery { UserTag = userTag }).Result;
                                            AlertDialogWindow alertDialogWindow = new AlertDialogWindow($"Founded user: {user.FirstName} {user.SecondName} {user.LastName}", "Complete", "Cancel");

                                            if (alertDialogWindow.ShowDialog().Value)
                                            {
                                                try
                                                {
                                                    await _mediator.Send(new AddUserInTeamCommand { TeamId = teamModel.TeamId, UserTag = userTag });
                                                }
                                                catch (Exception)
                                                {
                                                    ErrorWindow.Show("Error adding a user");
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            ErrorWindow.Show("User not found");
                                        }
                                    }
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

                                    await _mediator.Send(new UpdateTeamCommand { TeamId = teamModel.TeamId, LeaderTag = userTag, Name = teamModel.TeamName });

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


                        case "Create chat":

                            AlertDialogWindow alertDialogWindow2 = new AlertDialogWindow("Are you sure?", "Complete", "Cancel");

                            if (alertDialogWindow2.ShowDialog().Value)
                            {
                                await _mediator.Send(new CreateGroupChatByTeamCommand { TeamId = teamModel.TeamId, UserId = Properties.Settings.Default.userId });
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
