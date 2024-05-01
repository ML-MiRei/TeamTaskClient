using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
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
    public class ProjectsCash : IProjectsCash
    {

        public ProjectsCash(IProjectsEvents projectsEvents)
        {

            projectsEvents.NewProjectAdded += OnNewProjectAdded;
            projectsEvents.UserInProjectAdded += OnAddUserInProject;
            projectsEvents.ProjectDeleted += OnProjectDeleted;
            projectsEvents.UserFromProjectDeleted += OnUserFromProjectDeleted;
            projectsEvents.ProjectUpdated += OnProjectUpdated;


            projectsEvents.ProjectTaskUpdated += OnProjectTaskUpdated;
            projectsEvents.StatusProjectTaskChanged += OnStatusProjectTaskChanged;
            projectsEvents.ExecuterProjectTaskChanged += OnExecuterProjectTaskChanged;
            projectsEvents.ProjectTaskDeleted += OnProjectTaskDeleted;
            projectsEvents.ProjectTaskCreated += OnProjectTaskCreated;


            projectsEvents.SprintCreated += OnSprintCreated;
            projectsEvents.SprintDeleted += OnSprintDeleted;
            projectsEvents.DateEndSprintUpdated += OnDateEndSprintUpdated;
            projectsEvents.DateStartSprintUpdated += OnDateStartSprintUpdated;
        }

        private void OnDateStartSprintUpdated(object? sender, SprintEntity e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _projects.First(p => p.ProjectId == e.ProjectId).Sprints.First(s => s.SprintId == e.ID).DateStart = e.DateStart;

                if (SelectedProject.ProjectId == e.ProjectId)
                {
                    Sprints.First(s => s.SprintId == e.ID).DateStart = e.DateStart;

                    if (SelectedSprint.SprintId == e.ID)
                    {
                        SelectedSprint.DateStart = e.DateStart;
                        SprintChanged?.Invoke(sender, EventArgs.Empty);
                    }
                }

            });
        }

        private void OnDateEndSprintUpdated(object? sender, SprintEntity e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _projects.First(p => p.ProjectId == e.ProjectId).Sprints.First(s => s.SprintId == e.ID).DateEnd = e.DateEnd;

                if (SelectedProject.ProjectId == e.ProjectId)
                {
                    Sprints.First(s => s.SprintId == e.ID).DateEnd = e.DateEnd;

                    if (SelectedSprint.SprintId == e.ID)
                    {
                        SelectedSprint.DateEnd = e.DateEnd;
                        SprintChanged?.Invoke(sender, EventArgs.Empty);
                    }
                }
            });
        }

        private void OnSprintDeleted(object? sender, int e)
        {
            if (Projects.First(p => p.ProjectId == (int)sender).Sprints.First(s => s.SprintId == e).Tasks.Count > 0)
            {
                Projects.First(p => p.ProjectId == (int)sender).Sprints.First(s => s.SprintId == e).Tasks.ForEach(t =>
                Projects.First(p => p.ProjectId == (int)sender).Tasks.First(ts => ts.ProjectTaskId == t.ProjectTaskId).Status = (int)StatusProjectTaskEnum.STORIES);
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

        private void OnSprintCreated(object? sender, SprintModel e)
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





        private void OnProjectTaskCreated(object? sender, ProjectTaskEntity projectTaskEntity)
        {
            App.Current.Dispatcher.Invoke(() =>
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
            });
        }

        private void OnProjectTaskDeleted(object? sender, int projectTaskId)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var deletedTask = Projects.First(p => p.ProjectId == (int)sender).Tasks.First(t => t.ProjectTaskId == projectTaskId);

                if (SelectedProject?.ProjectId == (int)sender)
                {
                    BacklogTasks.Remove(deletedTask);

                    if (SelectedSprint != null && Tasks.Any(t => t.ProjectTaskId == (int)sender))
                    {
                        Tasks.Remove(deletedTask);
                    }

                }

                Projects.First(p => p.ProjectId == (int)sender).Tasks.Remove(deletedTask);
            });
        }

        private void OnExecuterProjectTaskChanged(object? sender, ProjectTaskEntity projectTaskEntity)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (SelectedProject?.ProjectId == projectTaskEntity.ProjectId)
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

                    TaskChanged?.Invoke(projectTaskEntity, EventArgs.Empty);
                }
                Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId).Tasks
                    .First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorName = projectTaskEntity.ExecutorName;
                Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId).Tasks
                    .First(t => t.ProjectTaskId == projectTaskEntity.ID).ExecutorTag = projectTaskEntity.ExecutorTag;

            });

        }

        private void OnStatusProjectTaskChanged(object? sender, ProjectTaskEntity projectTaskEntity)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (SelectedProject?.ProjectId == projectTaskEntity.ProjectId)
                {
                    BacklogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Status = projectTaskEntity.Status.Value;

                    if (SelectedSprint?.SprintId == projectTaskEntity.SprintId)
                    {
                        Tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Status = projectTaskEntity.Status.Value;
                    }

                    TaskChanged?.Invoke(projectTaskEntity, EventArgs.Empty);
                }

                Projects.First(p => p.ProjectId == projectTaskEntity.ProjectId)
                    .Tasks
                    .First(t => t.ProjectTaskId == projectTaskEntity.ID)
                    .Status = projectTaskEntity.Status.Value;

            });
        }

        private void OnProjectTaskUpdated(object? sender, ProjectTaskEntity projectTaskEntity)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (!String.IsNullOrEmpty(projectTaskEntity.Detail))
                    Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskEntity.ID).Details = projectTaskEntity.Detail;
                if (!String.IsNullOrEmpty(projectTaskEntity.Title))
                    Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskEntity.ID).Title = projectTaskEntity.Title;

                if (projectTaskEntity.ProjectId == SelectedProject?.ProjectId)
                {
                    if (!String.IsNullOrEmpty(projectTaskEntity.Detail))
                        _backlogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Details = projectTaskEntity.Detail;
                    if (!String.IsNullOrEmpty(projectTaskEntity.Title))
                        _backlogTasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Title = projectTaskEntity.Title;

                    if (SelectedProject.Sprints.Count > 0)
                    {
                        var sprint = SelectedProject.Sprints.First(s => s.Tasks.Any(t => t.ProjectTaskId == projectTaskEntity.ID));

                        if (SelectedSprint?.SprintId == sprint.SprintId)
                        {
                            if (!String.IsNullOrEmpty(projectTaskEntity.Detail))
                                _tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Details = projectTaskEntity.Detail;
                            if (!String.IsNullOrEmpty(projectTaskEntity.Title))
                                _tasks.First(t => t.ProjectTaskId == projectTaskEntity.ID).Title = projectTaskEntity.Title;
                        }
                    }
                    TaskChanged?.Invoke(projectTaskEntity, EventArgs.Empty);
                }
            });
        }





        private void OnProjectUpdated(object? sender, ProjectEntity e)
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

                    ProjectChanged?.Invoke(null, EventArgs.Empty);
                }

            });
        }

        private void OnUserFromProjectDeleted(object? sender, string e)
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

        private void OnProjectDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => _projects.Remove(_projects.First(p => p.ProjectId == e)));
        }

        private void OnAddUserInProject(object? sender, UserModel e)
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

        private void OnNewProjectAdded(object? sender, ProjectModel e)
        {
            App.Current.Dispatcher.Invoke(() => _projects.Add(e));
        }




        private static ObservableCollection<ProjectModel> _projects;
        public ObservableCollection<ProjectModel> Projects
        {
            get => _projects;
            set => _projects = value;
        }




        private static ObservableCollection<UserModel> _users = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set => _users = value;
        }


        private static ProjectModel _selectedProject;
        public ProjectModel SelectedProject
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
        public ObservableCollection<SprintModel> Sprints
        {
            get => _sprints;
            set => _sprints = value;
        }


        private static SprintModel _selectedSprint;
        public SprintModel SelectedSprint
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
        public ObservableCollection<ProjectTaskModel> BacklogTasks
        {
            get => _backlogTasks;
            set => _backlogTasks = value;
        }


        private static ObservableCollection<ProjectTaskModel> _tasks = new ObservableCollection<ProjectTaskModel>();
        public ObservableCollection<ProjectTaskModel> Tasks
        {
            get => _tasks;
            set => _tasks = value;
        }



        public event EventHandler<ProjectModel> SelectedProjectChanged;
        public event EventHandler<SprintModel> SelectedSprintChanged;
        public event EventHandler TaskChanged;
        public event EventHandler ProjectChanged;
        public event EventHandler SprintChanged;

    }
}
