using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.AddTeamProject;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.AddUserInProject;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.DeleteUserFromProject;
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.CreateProjectTask;
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.DeleteProjectTask;
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.SetExecutorProjectTask;
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.UpdateProjectTask;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.AddUserInTeam;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Queries.GetTeamsByProjectId;
using TeamTaskClient.ApplicationLayer.UseCases.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    public class BacklogProjectPageVM : ViewModelBase
    {
        private static IMediator _mediator;
        private static IProjectsCash _projectsCash;


        public BacklogProjectPageVM(IMediator mediator, IProjectsCash projectsCash)
        {
            _mediator = mediator;
            _projectsCash = projectsCash;

            AddProjectTaskInBacklog = new AddProjectTaskInBacklogCommand(this);
            AddUser = new AddUserCommand(this);

            _projectsCash.TaskChanged += OnTaskChanged;
        }

        private void OnTaskChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(BacklogProjectTasks));
        }



        public int UserRole => _projectsCash.SelectedProject.UserRole;

        public ObservableCollection<ProjectTaskModel> BacklogProjectTasks
        {
            get { return _projectsCash.BacklogTasks; }
        }

        public ObservableCollection<UserModel> Users
        {
            get { return _projectsCash.Users; }
        }



        public async void ActionWithUser(UserModel user)
        {
            if (_projectsCash.SelectedProject.UserRole == (int)UserRoleEnum.LEAD)
            {
                SelectActionsDialogWindow window = new SelectActionsDialogWindow("Select action", new List<string> { "Info", "Delete user" });
                if (window.ShowDialog().Value)
                {
                    if (window.GetSelectedAction() == "Info")
                    {
                        UserInfoWindow userInfoWindow = new UserInfoWindow(user);
                        userInfoWindow.ShowDialog();
                    }
                    else
                    {
                        AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                        if (alertDialogWindow.ShowDialog().Value)
                        {
                            try
                            {
                                await _mediator.Send(new DeleteUserFromProjectCommand { ProjectId = _projectsCash.SelectedProject.ProjectId, UserTag = user.UserTag });
                            }
                            catch
                            {
                                ErrorWindow.Show("Error user delete");
                            }
                        }
                    }
                }
            }
            else
            {
                UserInfoWindow userInfoWindow = new UserInfoWindow(user);
                userInfoWindow.ShowDialog();
            }
        }

        public async void ChangeTask(ProjectTaskModel projectTask)
        {
            if (projectTask.Status != (int)StatusProjectTaskEnum.DONE)
            {
                SelectActionsDialogWindow window = new SelectActionsDialogWindow("Select action", new List<string> { "Change info", "Set executor", "Delete" });
                if (window.ShowDialog().Value)
                {
                    var action = window.GetSelectedAction();
                    if (action == "Delete")
                    {
                        AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                        if (alertDialogWindow.ShowDialog().Value)
                        {

                            try
                            {
                                await _mediator.Send(new DeleteProjectTaskCommand { ProjectId = _projectsCash.SelectedProject.ProjectId, ProjectTaskId = projectTask.ProjectTaskId });
                            }
                            catch
                            {
                                ErrorWindow.Show("Error project task delete");
                            }

                        }
                    }
                    else if (action == "Change info")
                    {
                        InputDialogWindow inputDialogWindow = new InputDialogWindow("Enter new info", "Save", new List<string> { projectTask.Title, projectTask.Details });
                        if (inputDialogWindow.ShowDialog().Value)
                        {
                            var title = inputDialogWindow.GetInputValue()[0];
                            var details = inputDialogWindow.GetInputValue()[1];
                            try
                            {

                                if (!String.IsNullOrEmpty(title))
                                {
                                    if (!String.IsNullOrEmpty(details))
                                    {
                                        await _mediator.Send(new UpdateProjectTaskCommand
                                        {
                                            Detail = details,
                                            Title = title,
                                            ProjectTaskId = projectTask.ProjectTaskId,
                                            ProjectId = _projectsCash.SelectedProject.ProjectId
                                        });

                                    }
                                    else
                                    {

                                        await _mediator.Send(new UpdateProjectTaskCommand
                                        {
                                            Title = title,
                                            Detail = projectTask.Details,
                                            ProjectTaskId = projectTask.ProjectTaskId,
                                            ProjectId = _projectsCash.SelectedProject.ProjectId
                                        });
                                    }

                                }
                                else if (!String.IsNullOrEmpty(details))
                                {
                                    if (!String.IsNullOrEmpty(details))
                                    {
                                        await _mediator.Send(new UpdateProjectTaskCommand
                                        {
                                            Detail = details,
                                            Title = projectTask.Title,
                                            ProjectTaskId = projectTask.ProjectTaskId,
                                            ProjectId = _projectsCash.SelectedProject.ProjectId
                                        });
                                    }
                                }

                            }
                            catch
                            {
                                ErrorWindow.Show("Error project task update");
                            }
                        }
                    }
                    else
                    {
                        SelectUserWindow setExecutorWindow = new SelectUserWindow(_projectsCash);
                        if (setExecutorWindow.ShowDialog().Value)
                        {
                            var executor = setExecutorWindow.GetExecutor();
                            if (executor != null)
                            {
                                try
                                {
                                    await _mediator.Send(new SetExecutorProjectTaskCommand { ProjectTaskId = projectTask.ProjectTaskId, User = executor, ProjectId = _projectsCash.SelectedProject.ProjectId });
                                }
                                catch
                                {
                                    ErrorWindow.Show("Error set executor");
                                }
                            }
                        }
                    }
                }


            }

        }



        public ICommand AddProjectTaskInBacklog { get; }
        public ICommand AddUser { get; }

        private class AddProjectTaskInBacklogCommand(BacklogProjectPageVM vm) : CommandBase
        {
            public async override void Execute(object? parameter)
            {

                CreateSubjectDialogWindow createSubject = new CreateSubjectDialogWindow("Input task data", new List<string> { "Title: ", "Details: " });
                if (createSubject.ShowDialog().Value)
                {
                    var inputData = createSubject.GetCreatingProperties();

                    try
                    {
                        await _mediator.Send(new CreateProjectTaskCommand
                        {
                            Detail = inputData[1],
                            Title = inputData[0],
                            ProjectId = _projectsCash.SelectedProject.ProjectId,
                            Status = (int)StatusProjectTaskEnum.STORIES


                        });
                    }
                    catch
                    {
                        ErrorWindow.Show("Create task error");
                    }

                }
            }
        }


        private class AddUserCommand(BacklogProjectPageVM vm) : CommandBase
        {
            public override async void Execute(object? parameter)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Add user", "Add team" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    if (selectActionsDialogWindow.GetSelectedAction() == "Add user")
                    {

                        InputDialogWindow updatePropertiesDialogWindow = new InputDialogWindow("User tag", "Add", new List<string> { "" });
                        if (updatePropertiesDialogWindow.ShowDialog().Value)
                        {
                            var userTag = updatePropertiesDialogWindow.GetInputValue()[0];

                            if (userTag == Properties.Settings.Default.userTag)
                            {
                                ErrorWindow.Show("You can't add \nyourself to the team");
                            }
                            else
                            {
                                if (_projectsCash.SelectedProject.Users.Any(u => u.UserTag == userTag))
                                {
                                    ErrorWindow.Show("This user is already\n a member of the project");
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
                                                await _mediator.Send(new AddUserInProjectCommand { ProjectId = _projectsCash.SelectedProject.ProjectId, UserTag = userTag });
                                            }
                                            catch
                                            {
                                                ErrorWindow.Show("Error adding a user");
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        ErrorWindow.Show("User not found");
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        InputDialogWindow inputDialogWindow = new InputDialogWindow("Team tag", "Add", new List<string> { "" });
                        if (inputDialogWindow.ShowDialog().Value)
                        {
                            try
                            {
                                var teamTag = inputDialogWindow.GetInputValue()[0];
                                await _mediator.Send(new AddTeamProjectCommand { ProjectId = _projectsCash.SelectedProject.ProjectId, TeamTag = teamTag });
                            }
                            catch
                            {
                                ErrorWindow.Show("Add team error");

                            }
                        }


                    }

                }

            }
        }


    }
}
