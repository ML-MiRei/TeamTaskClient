using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Profile.ViewModels;

namespace TeamTaskClient.UI.Storages
{
    public class ProjectsStorage
    {

        static ProjectsStorage()
        {
            Sprints.CollectionChanged += OnSprintsCollectionChanged;
            Tasks.CollectionChanged += OnTasksCollectionChanged;
            Users.CollectionChanged += OnUsersCollectionChanged;


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

                if(SelectedProject.ProjectId == e.ProjectId)
                {
                    SelectedProject.Sprints.First(s => s.SprintId == e.ID).DateStart = e.DateStart;
                    SelectedProjectChanged?.Invoke(null, SelectedProject);
                }

                if (SelectedSprint.SprintId == e.ID)
                {
                    SelectedSprint.DateStart = e.DateStart;
                    SelectedSprintChanged?.Invoke(null, SelectedSprint);
                }

            });
        }

        private static void OnDateEndSprintUpdated(object? sender, SprintEntity e)
        {
            _projects.First(p => p.ProjectId == e.ProjectId).Sprints.First(s => s.SprintId == e.ID).DateEnd = e.DateEnd;

            if (SelectedProject.ProjectId == e.ProjectId)
            {
                SelectedProject.Sprints.First(s => s.SprintId == e.ID).DateEnd = e.DateEnd;
                SelectedProjectChanged?.Invoke(null, SelectedProject);
            }

            if (SelectedSprint.SprintId == e.ID)
            {
                SelectedSprint.DateEnd = e.DateEnd;
                SelectedSprintChanged?.Invoke(null, SelectedSprint);
            }
        }

        private static void OnSprintDeleted(object? sender, int e)
        {
            var sprint = _projects.First(p => p.ProjectId == (int)sender).Sprints.First(s => s.SprintId == e);
            _projects.First(p => p.ProjectId == (int)sender).Sprints.Remove(sprint);

            if (SelectedProject.ProjectId == (int)sender)
            {
                SelectedProject.Sprints.Remove(sprint);
                SelectedProjectChanged?.Invoke(null, SelectedProject);
            }

            if (SelectedSprint.SprintId == e)
            {
                ErrorWindow.Show("The selected sprint has been deleted");
                SelectedSprint = null;
                SelectedSprintChanged?.Invoke(null, SelectedSprint);
            }
        }

        private static void OnSprintCreated(object? sender, SprintModel e)
        {
            _projects.First(p => p.ProjectId == (int)sender).Sprints.Add(e);

            if (SelectedProject.ProjectId == (int)sender)
            {
                SelectedProject.Sprints.Add(e);
                SelectedProjectChanged?.Invoke(null, SelectedProject);
            }
        }

        private static void OnProjectTaskCreated(object? sender, ProjectTaskModel e)
        {
            App.Current.Dispatcher.Invoke(() => AddTasks(e));
        }

        private static void OnProjectTaskDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => RemoveTasks(e));
        }

        private static void OnExecuterProjectTaskChanged(object? sender, ProjectTaskModel e)
        {
            App.Current.Dispatcher.Invoke(() => ChangeExecutorProjectTask(e));

        }

        private static void OnStatusProjectTaskChanged(object? sender, ProjectTaskModel e)
        {
            App.Current.Dispatcher.Invoke(() => ChangeStatusProjectTask(e));
        }

        private static void OnProjectTaskUpdated(object? sender, ProjectTaskModel e)
        {
            App.Current.Dispatcher.Invoke(() => UpdateProjectTask(e));
        }

        private static void OnProjectUpdated(object? sender, ProjectModel e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {

                Projects.First(p => p.ProjectId == e.ProjectId).ProjectName = e.ProjectName;
                Projects.First(p => p.ProjectId == e.ProjectId).ProjectLeaderName = e.ProjectLeaderName;
                Projects.First(p => p.ProjectId == e.ProjectId).UserRole = e.UserRole;

                if (SelectedProject.ProjectId == e.ProjectId)
                {
                    SelectedProject.ProjectLeaderName = e.ProjectLeaderName;
                    SelectedProject.ProjectName = e.ProjectName;
                    SelectedProject.UserRole = e.UserRole;

                    SelectedProjectChanged?.Invoke(null, e);
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




        private static void OnUsersCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                SelectedProject.Users.Add((UserModel)sender);
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Users.Add((UserModel)sender);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                SelectedProject.Users.Remove((UserModel)sender);
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Users.Remove((UserModel)sender);
            }

            SelectedProjectChanged?.Invoke(sender, null);
        }


        private static void OnTasksCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                SelectedSprint.Tasks.Add((ProjectTaskModel)sender);
                SelectedProject.Tasks.Add((ProjectTaskModel)sender);
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Add((ProjectTaskModel)sender);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                SelectedSprint.Tasks.Remove((ProjectTaskModel)sender);
                SelectedProject.Tasks.Remove((ProjectTaskModel)sender);
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Remove((ProjectTaskModel)sender);
            }

            SelectedSprintChanged?.Invoke(sender, null);
        }

        private static void OnSprintsCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                SelectedProject.Sprints.Add((SprintModel)sender);
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Sprints.Add((SprintModel)sender);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                SelectedProject.Sprints.Remove((SprintModel)sender);
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Sprints.Remove((SprintModel)sender);
            }

            SelectedProjectChanged?.Invoke(sender, null);
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

                if (value.Tasks != null)
                    Tasks = new ObservableCollection<ProjectTaskModel>(value.Tasks);

                SelectedSprintChanged?.Invoke(SelectedSprintChanged, value);
            }
        }



        public static void AddBacklogTasks(ProjectTaskModel projectTaskModel)
        {
            _backlogTasks.Add(projectTaskModel);
            SelectedProject.Tasks.Add(projectTaskModel);
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Add(projectTaskModel);
            SelectedProjectChanged?.Invoke(projectTaskModel, null);
        }

        public static void RemoveBacklogTasks(ProjectTaskModel projectTaskModel)
        {
            _backlogTasks.Remove(projectTaskModel);
            _tasks.Remove(projectTaskModel);
            SelectedProject.Tasks.Remove(projectTaskModel);
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Remove(projectTaskModel);
            SelectedProjectChanged?.Invoke(projectTaskModel, null);
            SelectedSprintChanged?.Invoke(projectTaskModel, SelectedSprint);
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


        public static void ChangeStatusProjectTask(ProjectTaskModel projectTaskModel)
        {
            Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).Status = projectTaskModel.Status;
            SelectedSprint.Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).Status = projectTaskModel.Status;

            try
            {
                SelectedProject.Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).Status = projectTaskModel.Status;
                SelectedProjectChanged?.Invoke(projectTaskModel, SelectedProject);
            }
            catch (Exception) { }

            SelectedSprintChanged?.Invoke(projectTaskModel, SelectedSprint);
        }

        public static void ChangeExecutorProjectTask(ProjectTaskModel projectTaskModel)
        {
            Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
            Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).IsUserExecutor = projectTaskModel.IsUserExecutor;
            SelectedSprint.Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
            SelectedSprint.Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).IsUserExecutor = projectTaskModel.IsUserExecutor;

            try
            {
                SelectedProject.Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
                SelectedProject.Tasks.First(t => t.ProjectTaskId == projectTaskModel.ProjectTaskId).IsUserExecutor = projectTaskModel.IsUserExecutor;
                SelectedProjectChanged?.Invoke(projectTaskModel, SelectedProject);
            }
            catch (Exception) { }

            SelectedSprintChanged?.Invoke(projectTaskModel, SelectedSprint);
        }

        public static void UpdateProjectTask(ProjectTaskModel projectTaskModel)
        {
            try
            {
                if (!String.IsNullOrEmpty(projectTaskModel.Details))
                    SelectedSprint.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
                if (!String.IsNullOrEmpty(projectTaskModel.Title))
                    SelectedSprint.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
                if (!String.IsNullOrEmpty(projectTaskModel.ExecutorName))
                    SelectedSprint.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
            }
            catch (Exception) { }


            try
            {
                if (!String.IsNullOrEmpty(projectTaskModel.Details))
                    BacklogTasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
                if (!String.IsNullOrEmpty(projectTaskModel.Title))
                    BacklogTasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
                if (!String.IsNullOrEmpty(projectTaskModel.ExecutorName))
                    BacklogTasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
            }
            catch (Exception) { }

            if (!String.IsNullOrEmpty(projectTaskModel.Details))
                SelectedProject.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
            if (!String.IsNullOrEmpty(projectTaskModel.Title))
                SelectedProject.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
            if (!String.IsNullOrEmpty(projectTaskModel.ExecutorName))
                SelectedProject.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;

            if (!String.IsNullOrEmpty(projectTaskModel.Details))
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
            if (!String.IsNullOrEmpty(projectTaskModel.Title))
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
            if (!String.IsNullOrEmpty(projectTaskModel.ExecutorName))
                Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;

            SelectedProjectChanged?.Invoke(null, SelectedProject);
        }

        public static void AddTasks(ProjectTaskModel projectTaskModel)
        {
            _tasks.Add(projectTaskModel);
            SelectedSprint.Tasks.Add(projectTaskModel);
            SelectedProject.Tasks.Add(projectTaskModel);
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Add(projectTaskModel);

            SelectedProjectChanged?.Invoke(projectTaskModel, null);
            SelectedSprintChanged?.Invoke(projectTaskModel, null);
        }

        public static void RemoveTasks(int projectTaskId)
        {
            var projectTaskModel = _tasks.First(t => t.ProjectTaskId == projectTaskId);

            _tasks.Remove(projectTaskModel);
            SelectedSprint.Tasks.Remove(projectTaskModel);
            SelectedProject.Tasks.Remove(projectTaskModel);
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Remove(projectTaskModel);

            SelectedProjectChanged?.Invoke(projectTaskModel, null);
            SelectedSprintChanged?.Invoke(projectTaskModel, null);
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
