using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.AddTeamProject;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.AddUserInProject;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteUserFromProject;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.DeleteProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.SetExecutorProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.UpdateProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.AddUserInTeam;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByProjectId;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    public class BacklogProjectPageVM : ViewModelBase
    {
        private static IMediator _mediator;


        public BacklogProjectPageVM(IMediator mediator)
        {
            _mediator = mediator;

            AddProjectTaskInBacklog = new AddProjectTaskInBacklogCommand(this);
            AddUser = new AddUserCommand(this);

            ProjectsStorage.SelectedProjectChanged += OnSelectedProjectChanged;

        }


        public int UserRole => ProjectsStorage.SelectedProject.UserRole;

        private void OnSelectedProjectChanged(object? sender, ProjectModel e)
        {
            OnPropertyChanged(nameof(BacklogProjectTasks));
        }


        public ObservableCollection<ProjectTaskModel> BacklogProjectTasks
        {
            get { return ProjectsStorage.BacklogTasks; }
        }

        public ObservableCollection<UserModel> Users
        {
            get { return ProjectsStorage.Users; }
        }



        public async void ActionWithUser(UserModel user)
        {
            SelectActionsDialogWindow window = new SelectActionsDialogWindow("Select action", new List<string> { "Delete user" });
            if (window.ShowDialog().Value)
            {


                AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                if (alertDialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        await _mediator.Send(new DeleteUserFromProjectCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId, UserTag = user.UserTag });
                        ProjectsStorage.Users.Remove(user);
                    }
                    catch
                    {
                        ErrorWindow.Show("Error user delete");
                    }

                }

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
                                await _mediator.Send(new DeleteProjectTaskCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId, ProjectTaskId = projectTask.ProjectTaskId });
                                ProjectsStorage.RemoveBacklogTasks(projectTask);
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
                                        await _mediator.Send(new UpdateProjectTaskCommand { Detail = details, Title = title, ProjectTaskId = projectTask.ProjectTaskId });
                                        projectTask.Details = details;
                                        projectTask.Title = title;
                                        ProjectsStorage.UpdateProjectTask(projectTask);

                                        OnPropertyChanged(nameof(BacklogProjectTasks));
                                    }
                                    else
                                    {

                                        await _mediator.Send(new UpdateProjectTaskCommand { Title = title, Detail = projectTask.Details, ProjectTaskId = projectTask.ProjectTaskId });
                                        projectTask.Title = title;
                                        ProjectsStorage.UpdateProjectTask(projectTask);
                                    }

                                }
                                else if (!String.IsNullOrEmpty(details))
                                {
                                    if (!String.IsNullOrEmpty(details))
                                    {
                                        await _mediator.Send(new UpdateProjectTaskCommand { Detail = details, Title = projectTask.Title, ProjectTaskId = projectTask.ProjectTaskId });
                                        projectTask.Details = details;
                                        ProjectsStorage.UpdateProjectTask(projectTask);
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
                        SelectUserWindow setExecutorWindow = new SelectUserWindow();
                        if (setExecutorWindow.ShowDialog().Value)
                        {
                            var executor = setExecutorWindow.GetExecutor();
                            if (executor != null)
                            {
                                try
                                {
                                    await _mediator.Send(new SetExecutorProjectTaskCommand { ProjectTaskId = projectTask.ProjectTaskId, User = executor });

                                    projectTask.ExecutorName = executor.FirstName;
                                    ProjectsStorage.UpdateProjectTask(projectTask);
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
                            ProjectId = ProjectsStorage.SelectedProject.ProjectId,
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
            public override void Execute(object? parameter)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Add user", "Add team" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    if (selectActionsDialogWindow.GetSelectedAction() == "Add user")
                    {

                        InputDialogWindow updatePropertiesDialogWindow = new InputDialogWindow("User tag", "Add", new List<string> { "" });
                        if (updatePropertiesDialogWindow.ShowDialog().Value)
                        {
                            try
                            {
                                var userTag = updatePropertiesDialogWindow.GetInputValue()[0];
                                _mediator.Send(new AddUserInProjectCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId, UserTag = userTag });

                                var newUser = _mediator.Send(new GetUserByTagQuery { UserTag = userTag }).Result;

                                var user = new UserModel
                                {
                                    Email = newUser.Email,
                                    UserTag = userTag,
                                    SecondName = newUser.SecondName,
                                    FirstName = newUser.FirstName,
                                    LastName = newUser.LastName,
                                    PhoneNumber = newUser.PhoneNumber
                                };

                                ProjectsStorage.Users.Add(user);
                            }
                            catch
                            {
                                ErrorWindow.Show("Add user error");

                            }

                        }
                        else
                        {
                            InputDialogWindow updateDialogWindow = new InputDialogWindow("Team tag", "Add", new List<string> { "" });
                            if (updatePropertiesDialogWindow.ShowDialog().Value)
                            {
                                try
                                {
                                    var teamTag = updatePropertiesDialogWindow.GetInputValue()[0];
                                    _mediator.Send(new AddTeamProjectCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId, TeamTag = teamTag });

                                    var newTeam = _mediator.Send(new GetTeamsByProjectIdCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId }).Result
                                        .First(p => p.TeamTag == teamTag);

                                    foreach (var userInTeam in newTeam.Users)
                                    {


                                        ProjectsStorage.Users.Add(new UserModel
                                        {
                                            Email = userInTeam.Email,
                                            UserTag = userInTeam.UserTag,
                                            SecondName = userInTeam.SecondName,
                                            FirstName = userInTeam.FirstName,
                                            LastName = userInTeam.LastName,
                                            PhoneNumber = userInTeam.PhoneNumber
                                        });
                                    }
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
}
