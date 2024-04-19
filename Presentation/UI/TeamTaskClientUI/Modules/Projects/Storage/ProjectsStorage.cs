using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.UI.Modules.Projects.Storage
{
    internal class ProjectsStorage
    {

        static ProjectsStorage()
        {
            Sprints.CollectionChanged += OnSprintsCollectionChanged;
            Tasks.CollectionChanged += OnTasksCollectionChanged;
            Users.CollectionChanged += OnUsersCollectionChanged;
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


        public static void UpdateProjectTask(ProjectTaskModel projectTaskModel)
        {
            try
            {
                SelectedSprint.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
                SelectedSprint.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
                SelectedSprint.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
            }
            catch (Exception) { }

            SelectedProject.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
            SelectedProject.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
            SelectedProject.Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;

            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Details = projectTaskModel.Details;
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).Title = projectTaskModel.Title;
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.First(p => p.ProjectTaskId == projectTaskModel.ProjectTaskId).ExecutorName = projectTaskModel.ExecutorName;
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
            SelectedProject.Tasks.Remove(projectTaskModel);
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Remove(projectTaskModel);
            SelectedProjectChanged?.Invoke(projectTaskModel, null);
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



        public static void AddTasks(ProjectTaskModel projectTaskModel)
        {
            _tasks.Add(projectTaskModel);
            SelectedSprint.Tasks.Add(projectTaskModel);
            SelectedProject.Tasks.Add(projectTaskModel);
            Projects.First(p => p.ProjectId == SelectedProject.ProjectId).Tasks.Add(projectTaskModel);

            SelectedProjectChanged?.Invoke(projectTaskModel, null);
            SelectedSprintChanged?.Invoke(projectTaskModel, null);
        }

        public static void RemoveTasks(ProjectTaskModel projectTaskModel)
        {
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
