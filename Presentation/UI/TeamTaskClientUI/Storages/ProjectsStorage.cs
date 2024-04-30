using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Profile.ViewModels;
using TeamTaskClient.UI.Modules.Projects.View;

namespace TeamTaskClient.UI.Storages
{
    public class ProjectsStorage
    {
        public static event EventHandler BacklogInterfaceRefresh;
        public static event EventHandler TasksInterfaceRefresh;

        static ProjectsStorage()
        {

            ProjectHubClient.NewProjectAdded += OnNewProjectAdded;
            ProjectHubClient.UserInProjectAdded += OnAddUserInProject;
            ProjectHubClient.ProjectDeleted += OnProjectDeleted;
            ProjectHubClient.UserFromProjectDeleted += OnUserFromProjectDeleted;
            ProjectHubClient.ProjectUpdated += OnProjectUpdated;


            ProjectHubClient.ProjectTaskUpdated += OnProjectTaskUpdated;
            ProjectHubClient.StatusProjectTaskChanged += OnStatusProjectTaskChanged;
            ProjectHubClient.ExecuterProjectTaskChanged += OnExecuterProjectTaskChanged;
            ProjectHubClient.ProjectTaskDeleted += OnProjectTaskDeleted;
            ProjectHubClient.ProjectTaskCreated += OnProjectTaskCreated;


            ProjectHubClient.SprintCreated += OnSprintCreated;
            ProjectHubClient.SprintDeleted += OnSprintDeleted;
            ProjectHubClient.DateEndSprintUpdated += OnDateEndSprintUpdated;
            ProjectHubClient.DateStartSprintUpdated += OnDateStartSprintUpdated;
        }

        private static void OnDateStartSprintUpdated(object? sender, SprintEntity e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _projects.First(p => p.ProjectId == e.ProjectId).Sprints.First(s => s.SprintId == e.ID).DateStart = e.DateStart;

                if (SelectedProject.ProjectId == e.ProjectId)
                {
                    Sprints.First(s => s.SprintId == e.ID).DateStart = e.DateStart;
                }

                if (SelectedSprint.SprintId == e.ID)
                {
                    SelectedSprint.DateStart = e.DateStart;
                }

                TasksInterfaceRefresh?.Invoke(sender, EventArgs.Empty);
            });
        }

        private static void OnDateEndSprintUpdated(object? sender, SprintEntity e)
        {
            _projects.First(p => p.ProjectId == e.ProjectId).Sprints.First(s => s.SprintId == e.ID).DateEnd = e.DateEnd;

            if (SelectedProject.ProjectId == e.ProjectId)
            {
                Sprints.First(s => s.SprintId == e.ID).DateEnd = e.DateEnd;
            }

            if (SelectedSprint.SprintId == e.ID)
            {
                SelectedSprint.DateEnd = e.DateEnd;
            }
            TasksInterfaceRefresh?.Invoke(sender, EventArgs.Empty);

        }

        private static void OnSprintDeleted(object? sender, int e)
        {


            if (Projects.First(p => p.ProjectId == (int)sender).Sprints.First(s => s.SprintId == e).Tasks.Count > 0)
            {
                Projects.First(p => p.ProjectId == (int)sender).Sprints.First(s => s.SprintId == e).Tasks.ForEach(t =>
                Projects.First(p => p.ProjectId == (int)sender).Tasks.First(ts => ts.ProjectTaskId == t.ProjectTaskId).Status = (int)StatusProjectTaskEnum.STORIES
                );
            }


            var sprint = _projects.First(p => p.ProjectId == (int)sender).Sprints.First(s => s.SprintId == e);
            _projects.First(p => p.ProjectId == (int)sender).Sprints.Remove(sprint);


            if (SelectedProject.ProjectId == (int)sender)
            {
                Sprints.Remove(sprint);
                SelectedProjectChanged?.Invoke(null, SelectedProject);
            }

            if (SelectedSprint.SprintId == e)
            {

                App.Current.Dispatcher.Invoke(() =>
                {
                    ErrorWindow.Show("The selected sprint has been deleted");
                    if (SelectedProject.Sprints.Count > 0)
                    {
                        SelectedSprint = SelectedProject.Sprints.First(s => s.DateStart <= DateTime.Now && s.DateEnd >= DateTime.Now);
                    }
                    else
                    {
                        SelectedSprint = null;
                    }
                });


            }
        }

        private static void OnSprintCreated(object? sender, SprintModel e)
        {
            _projects.First(p => p.ProjectId == (int)sender).Sprints.Add(e);

            if (SelectedProject.ProjectId == (int)sender)
            {
                Sprints.Add(e);

                if (SelectedProject.UserRole == (int)UserRoleEnum.LEAD)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        SelectedSprint = e;
                        ProjectPage.Instance.ToTasksButton.IsChecked = true;
                    });

                }
                App.Current.Dispatcher.Invoke(() => SelectedProjectChanged?.Invoke(null, SelectedProject));
            }

        }





        private static void OnProjectTaskCreated(object? sender, ProjectTaskEntity e)
        {
            App.Current.Dispatcher.Invoke(() => AddTasks(e));
        }

        private static void OnProjectTaskDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => RemoveTasks((int)sender, e));
            TasksInterfaceRefresh?.Invoke(e, EventArgs.Empty);
        }

        private static void OnExecuterProjectTaskChanged(object? sender, ProjectTaskEntity e)
        {
            App.Current.Dispatcher.Invoke(() => ChangeExecutorProjectTask(e));
            BacklogInterfaceRefresh?.Invoke(e, EventArgs.Empty);
            TasksInterfaceRefresh?.Invoke(e, EventArgs.Empty);

        }

        private static void OnStatusProjectTaskChanged(object? sender, ProjectTaskEntity e)
        {
            App.Current.Dispatcher.Invoke(() => ChangeStatusProjectTask(e));
            TasksInterfaceRefresh?.Invoke(e, EventArgs.Empty);
        }

        private static void OnProjectTaskUpdated(object? sender, ProjectTaskEntity e)
        {
            App.Current.Dispatcher.Invoke(() => UpdateProjectTask(e));
            BacklogInterfaceRefresh?.Invoke(e, EventArgs.Empty);
        }





        private static void OnProjectUpdated(object? sender, ProjectEntity e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (e.ProjectName != null)
                    Projects.First(p => p.ProjectId == e.ID).ProjectName = e.ProjectName;
                if (e.ProjectLeadName != null)
                    Projects.First(p => p.ProjectId == e.ID).ProjectLeaderName = e.ProjectLeadName;
                if (e.ProjectLeadTag != null)
                    Projects.First(p => p.ProjectId == e.ID).UserRole = e.ProjectLeadTag == Properties.Settings.Default.userTag ? (int)UserRoleEnum.LEAD : (int)UserRoleEnum.EMPLOYEE;

                if (SelectedProject.ProjectId == e.ID)
                {
                    if (e.ProjectLeadName != null)
                        SelectedProject.ProjectLeaderName = e.ProjectLeadName;
                    if (e.ProjectName != null)
                        SelectedProject.ProjectName = e.ProjectName;
                    if (e.ProjectLeadTag != null)
                        SelectedProject.UserRole = e.ProjectLeadTag == Properties.Settings.Default.userTag ? (int)UserRoleEnum.LEAD : (int)UserRoleEnum.EMPLOYEE;

                    SelectedProjectChanged?.Invoke(null, new ProjectModel
                    {
                        ProjectId = e.ID,
                        ProjectLeaderName = e.ProjectLeadName,
                        ProjectName = e.ProjectName,
                        UserRole = e.ProjectLeadTag == Properties.Settings.Default.userTag ? (int)UserRoleEnum.LEAD : (int)UserRoleEnum.EMPLOYEE
                    });
                }

            });
        }

        private static void OnUserFromProjectDeleted(object? sender, string e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var deletedUser = Projects.First(p => p.ProjectId == (int)sender).Users.First(u => u.UserTag == e);

                Projects.First(p => p.ProjectId == (int)sender).Users.Remove(deletedUser);
                if (SelectedProject.ProjectId == (int)sender)
                {
                    SelectedProject.Users.Remove(deletedUser);
                    Users.Remove(deletedUser);
                    SelectedProjectChanged?.Invoke(null, SelectedProject);
                }

            });
        }

        private static void OnProjectDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => _projects.Remove(_projects.First(p => p.ProjectId == e)));
        }

        private static void OnAddUserInProject(object? sender, UserModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Projects.First(p => p.ProjectId == (int)sender).Users.Add(e);
                if (SelectedProject.ProjectId == (int)sender)
                {
                    SelectedProject.Users.Add(e);
                    Users.Add(e);
                    SelectedProjectChanged?.Invoke(null, SelectedProject);
                }

            });
        }

        private static void OnNewProjectAdded(object? sender, ProjectModel e)
        {
            App.Current.Dispatcher.Invoke(() => _projects.Add(e));
        }




        private static ObservableCollection<ProjectModel> _projects;
        public static ObservableCollection<ProjectModel> Projects
        {
            get => _projects;

            set
            {

                _projects = value;

            }
        }




        private static ObservableCollection<UserModel> _users = new ObservableCollection<UserModel>();
        public static ObservableCollection<UserModel> Users
        {
            get => _users;
            set => _users = value;
        }


        private static ProjectModel _selectedProject;
        public static ProjectModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;

                try
                {
                    if (value.Sprints.Count != 0)
                        SelectedSprint = value.Sprints.First(s => s.DateStart <= DateTime.Now && s.DateEnd >= DateTime.Now);
                }
                catch
                {
                    SelectedSprint = value.Sprints[0];
                }

                if (value.Users != null)
                    Users = new ObservableCollection<UserModel>(value.Users);

                if (value.Sprints != null)
                    Sprints = new ObservableCollection<SprintModel>(value.Sprints);

                if (value.Tasks != null)
                    BacklogTasks = new ObservableCollection<ProjectTaskModel>(value.Tasks);

                SelectedProjectChanged?.Invoke(SelectedProjectChanged, value);
            }
        }





        private static ObservableCollection<SprintModel> _sprints = new ObservableCollection<SprintModel>();
        public static ObservableCollection<SprintModel> Sprints
        {
            get => _sprints;

            set
            {
                _sprints = value;
            }
        }


        private static SprintModel _selectedSprint;
        public static SprintModel SelectedSprint
        {
            get => _selectedSprint;
            set
            {
                _selectedSprint = value;

                if (value != null && value.Tasks != null)
                    Tasks = new ObservableCollection<ProjectTaskModel>(value.Tasks);

                SelectedSprintChanged?.Invoke(SelectedSprintChanged, value);
            }
        }



        private static ObservableCollection<ProjectTaskModel> _backlogTasks = new ObservableCollection<ProjectTaskModel>();
        public static ObservableCollection<ProjectTaskModel> BacklogTasks
        {
            get => _backlogTasks;

            set
            {
                _backlogTasks = value;
            }
        }


        public static void ChangeStatusProjectTask(ProjectTaskEntity projectTaskEntity)
        {

            if (projectTaskEntity.ProjectId == SelectedProject?.ProjectId)
            {
                BacklogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Status = projectTaskEntity.Status.Value;

                if (SelectedSprint?.SprintId == projectTaskEntity.SprintId)
                {
                    Tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Status = projectTaskEntity.Status.Value;
                }

            }

            Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId)
                .Tasks
                .First(t => t.ProjectTaskId == projectTaskEntity.ID)
                .Status = projectTaskEntity.Status.Value;


        }

        public static void ChangeExecutorProjectTask(ProjectTaskEntity projectTaskEntity)
        {

            if (projectTaskEntity.ProjectId == SelectedProject?.ProjectId)
            {
                BacklogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorName = projectTaskEntity.ExecutorName;
                BacklogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorTag = projectTaskEntity.ExecutorTag;


                if (SelectedProject.Sprints.Count > 0)
                {
                    if (SelectedSprint != null)
                    {
                        var sprint = SelectedProject.Sprints.First(s => s.Tasks.Any(t => t.ProjectTaskId == projectTaskEntity.ID));

                        if (SelectedSprint.SprintId == sprint.SprintId)
                        {
                            Tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorName = projectTaskEntity.ExecutorName;
                            Tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorTag = projectTaskEntity.ExecutorTag;
                        }

                    }

                }
            }
            Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId).Tasks
                .First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorName = projectTaskEntity.ExecutorName;

            Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId).Tasks
                .First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorTag = projectTaskEntity.ExecutorTag;
        }

        public static void UpdateProjectTask(ProjectTaskEntity projectTaskEntity)
        {

            if (projectTaskEntity.ProjectId == SelectedProject?.ProjectId)
            {
                if (!String.IsNullOrEmpty(projectTaskEntity.Detail))
                    BacklogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Details = projectTaskEntity.Detail;
                if (!String.IsNullOrEmpty(projectTaskEntity.Title))
                    BacklogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Title = projectTaskEntity.Title;

                if (SelectedProject.Sprints.Count > 0)
                {

                    var sprint = SelectedProject.Sprints.First(s => s.Tasks.Any(t => t.ProjectTaskId == projectTaskEntity.ID));

                    if (SelectedSprint?.SprintId == sprint.SprintId)
                    {
                        if (!String.IsNullOrEmpty(projectTaskEntity.Detail))
                            Tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Details = projectTaskEntity.Detail;
                        if (!String.IsNullOrEmpty(projectTaskEntity.Title))
                            Tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Title = projectTaskEntity.Title;
                    }
                }
            }

            if (!String.IsNullOrEmpty(projectTaskEntity.Detail))
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskEntity.ID).Details = projectTaskEntity.Detail;
            if (!String.IsNullOrEmpty(projectTaskEntity.Title))
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskEntity.ID).Title = projectTaskEntity.Title;

        }

        public static void AddTasks(ProjectTaskEntity projectTaskEntity)
        {
            var projectTaskModel = new ProjectTaskModel
            {
                Status = projectTaskEntity.Status.Value,
                Details = projectTaskEntity.Detail,
                Title = projectTaskEntity.Title,
                ExecutorName = null,
                ExecutorTag = null,
                ProjectTaskId = projectTaskEntity.ID
            };


            if (SelectedProject?.ProjectId == projectTaskEntity.ProjectId)
            {
                BacklogTasks.Add(projectTaskModel);

                if (SelectedSprint?.SprintId == projectTaskEntity.SprintId)
                {
                    Tasks.Add(projectTaskModel);
                }

            }

            Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId).Tasks.Add(projectTaskModel);
        }

        public static void RemoveTasks(int projectId, int projectTaskId)
        {

            var deletedTask = Projects.First(p => p.ProjectId == projectId).Tasks.First(t => t.ProjectTaskId == projectTaskId);

            if (SelectedProject?.ProjectId == projectId)
            {
                BacklogTasks.Remove(deletedTask);

                if (SelectedSprint != null && Tasks.Any(t => t.ProjectTaskId == projectId))
                {
                    Tasks.Remove(deletedTask);
                }

            }


            Projects.First(p => p.ProjectId == projectId).Tasks.Remove(deletedTask);

        }


        private static ObservableCollection<ProjectTaskModel> _tasks = new ObservableCollection<ProjectTaskModel>();
        public static ObservableCollection<ProjectTaskModel> Tasks
        {
            get => _tasks;

            set
            {
                _tasks = value;
            }
        }



        public static event EventHandler<ProjectModel> SelectedProjectChanged;
        public static event EventHandler<SprintModel> SelectedSprintChanged;
    }
}
