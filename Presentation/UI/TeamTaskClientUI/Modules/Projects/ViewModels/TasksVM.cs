using MediatR;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.AddInSprintProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.ChangeStatusProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class TasksVM : ViewModelBase
    {
        private static IMediator _mediator;



        public TasksVM(IMediator mediator)
        {
            _mediator = mediator;

            SelectSprint = new SelectSprintCommand(this);

            ProjectsStorage.SelectedSprintChanged += OnSelectedSprintChanged;
        }

        private void OnSelectedSprintChanged(object? sender, SprintModel e)
        {
            //OnPropertyChanged(nameof(SprintNumber));
            //OnPropertyChanged(nameof(ProjectTasksDone));
            //OnPropertyChanged(nameof(ProjectTasksInProcess));
            //OnPropertyChanged(nameof(ProjectTasksTesting));
            //OnPropertyChanged(nameof(ProjectTasksTodo));
        }


        public int UserRole => ProjectsStorage.SelectedProject.UserRole;


        public string SprintNumber { get => (ProjectsStorage.Sprints.IndexOf(ProjectsStorage.SelectedSprint) + 1).ToString(); }


        public event EventHandler<ProjectTaskModel> ChangeStatus;
        public static event EventHandler<SprintModel> AddSprintEvent;


        private ObservableCollection<ProjectTaskModel> _projectTasksTodo;
        public ObservableCollection<ProjectTaskModel> ProjectTasksTodo
        {
            get { return new ObservableCollection<ProjectTaskModel>(ProjectsStorage.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.TODO)); }
            set { _projectTasksTodo = value; }
        }



        private ObservableCollection<ProjectTaskModel> _projectTasksInProcess;
        public ObservableCollection<ProjectTaskModel> ProjectTasksInProcess
        {
            get
            {
                return new ObservableCollection<ProjectTaskModel>(ProjectsStorage.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.IN_PROGRESS));
            }


            set { _projectTasksInProcess = value; }
        }


        private ObservableCollection<ProjectTaskModel> _projectTasksTesting;
        public ObservableCollection<ProjectTaskModel> ProjectTasksTesting
        {
            get { return new ObservableCollection<ProjectTaskModel>(ProjectsStorage.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.TESTING)); }
            set { _projectTasksTesting = value; }
        }



        private ObservableCollection<ProjectTaskModel> _projectTasksDone;
        public ObservableCollection<ProjectTaskModel> ProjectTasksDone
        {
            get { return new ObservableCollection<ProjectTaskModel>(ProjectsStorage.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.DONE)); }
            set { _projectTasksDone = value; }
        }



        public string DateStart => ProjectsStorage.SelectedSprint.DateStart.ToString("d");
        public string DateEnd => ProjectsStorage.SelectedSprint.DateEnd.ToString("d");


        //public ICommand AddProjectTaskTodo { get; }
        //public ICommand AddProjectTaskInProcess { get; }
        //public ICommand AddProjectTaskTesting { get; }
        //public ICommand AddProjectTaskDone { get; }


        public ICommand AddSprint { get; } = new AddSprintCommand();
        public ICommand SelectSprint { get; }


        //private class AddProjectTaskTodoCommand(TasksVM vm) : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        CreateSubjectDialogWindow createSubject = new CreateSubjectDialogWindow("Input task data", new List<string> { "Title: ", "Details: " });
        //        if (createSubject.ShowDialog().Value)
        //        {
        //            var inputData = createSubject.GetCreatingProperties();

        //            try
        //            {
        //                var projectTask = _mediator.Send(new CreateProjectTaskCommand
        //                {
        //                    Detail = inputData[1],
        //                    Title = inputData[0],
        //                    SprintId = ProjectsStorage.SelectedSprint.SprintId,
        //                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
        //                    Status = (int)StatusProjectTaskEnum.TODO


        //                }).Result;

        //                ProjectsStorage.SelectedSprint.Tasks.Add(projectTask);

        //                if (vm.ProjectTasksTodo == null)
        //                    vm.ProjectTasksTodo = new ObservableCollection<ProjectTaskModel>();


        //                vm.ProjectTasksTodo.Add(projectTask);

        //                ProjectsStorage.Tasks.Add(projectTask);




        //            }
        //            catch
        //            {
        //                ErrorWindow.Show("Create task error");
        //            }

        //        }
        //    }
        //}

        //private class AddProjectTaskInProcessCommand(TasksVM vm) : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        CreateSubjectDialogWindow createSubject = new CreateSubjectDialogWindow("Input task data", new List<string> { "Title: ", "Details: " });
        //        if (createSubject.ShowDialog().Value)
        //        {
        //            var inputData = createSubject.GetCreatingProperties();

        //            try
        //            {
        //                var projectTask = _mediator.Send(new CreateProjectTaskCommand
        //                {
        //                    Detail = inputData[1],
        //                    Title = inputData[0],
        //                    SprintId = ProjectsStorage.SelectedSprint.SprintId,
        //                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
        //                    Status = (int)StatusProjectTaskEnum.IN_PROGRESS
        //                }).Result;

        //                ProjectsStorage.SelectedSprint.Tasks.Add(projectTask);

        //                if (vm.ProjectTasksInProcess == null)
        //                    vm.ProjectTasksInProcess = new ObservableCollection<ProjectTaskModel>();


        //                vm.ProjectTasksInProcess.Add(projectTask);

        //                ProjectsStorage.Tasks.Add(projectTask);

        //            }
        //            catch
        //            {
        //                ErrorWindow.Show("Create task error");
        //            }

        //        }
        //    }
        //}

        //private class AddProjectTaskTestingCommand(TasksVM vm) : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        CreateSubjectDialogWindow createSubject = new CreateSubjectDialogWindow("Input task data", new List<string> { "Title: ", "Details: " });
        //        if (createSubject.ShowDialog().Value)
        //        {
        //            var inputData = createSubject.GetCreatingProperties();

        //            try
        //            {
        //                var projectTask = _mediator.Send(new CreateProjectTaskCommand
        //                {
        //                    Detail = inputData[1],
        //                    Title = inputData[0],
        //                    SprintId = ProjectsStorage.SelectedSprint.SprintId,
        //                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
        //                    Status = (int)StatusProjectTaskEnum.TESTING
        //                }).Result;

        //                ProjectsStorage.SelectedSprint.Tasks.Add(projectTask);

        //                if (vm.ProjectTasksTesting == null)
        //                    vm.ProjectTasksTesting = new ObservableCollection<ProjectTaskModel>();

        //                vm.ProjectTasksTesting.Add(projectTask);

        //                ProjectsStorage.Tasks.Add(projectTask);

        //            }
        //            catch
        //            {
        //                ErrorWindow.Show("Create task error");
        //            }

        //        }
        //    }
        //}

        //private class AddProjectTaskDoneCommand(TasksVM vm) : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        CreateSubjectDialogWindow createSubject = new CreateSubjectDialogWindow("Input task data", new List<string> { "Title: ", "Details: " });
        //        if (createSubject.ShowDialog().Value)
        //        {
        //            var inputData = createSubject.GetCreatingProperties();

        //            try
        //            {
        //                var projectTask = _mediator.Send(new CreateProjectTaskCommand
        //                {
        //                    Detail = inputData[1],
        //                    Title = inputData[0],
        //                    SprintId = ProjectsStorage.SelectedSprint.SprintId,
        //                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
        //                    Status = (int)StatusProjectTaskEnum.DONE

        //                }).Result;

        //                ProjectsStorage.SelectedSprint.Tasks.Add(projectTask);

        //                if (vm.ProjectTasksDone == null)
        //                    vm.ProjectTasksDone = new ObservableCollection<ProjectTaskModel>();

        //                vm.ProjectTasksDone.Add(projectTask);

        //                ProjectsStorage.Tasks.Add(projectTask);

        //            }
        //            catch
        //            {
        //                ErrorWindow.Show("Create task error");
        //            }

        //        }
        //    }
        //}


        private class SelectSprintCommand(TasksVM vm) : CommandBase
        {
            public override void Execute(object? parameter)
            {

                List<string> list = new List<string>();

                for (int i = 0; i < ProjectsStorage.Sprints.Count; i++)
                {
                    list.Add($"Sprint №{i + 1}, {ProjectsStorage.Sprints[i].DateStart.ToString("d")} - {ProjectsStorage.Sprints[i].DateEnd.ToString("d")}");
                }


                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select sprint", list);

                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    var selectedSprint = ProjectsStorage.Sprints[list.IndexOf(selectActionsDialogWindow.GetSelectedAction())];
                    ProjectsStorage.SelectedSprint = selectedSprint;

                }


            }
        }

        private class AddSprintCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                CreateSprintWindow createSprintWindow = new CreateSprintWindow();
                if (createSprintWindow.ShowDialog().Value)
                {

                    var newSprint = createSprintWindow.GetSprintModel();
                    try
                    {

                        var sprint = _mediator.Send(new CreateSprintCommand
                        {
                            SprintEntity = new Domain.Entities.SprintEntity
                            {
                                DateEnd = newSprint.DateEnd,
                                DateStart = newSprint.DateStart,
                                ProjectId = ProjectsStorage.SelectedProject.ProjectId,
                            }
                        }).Result;

                        ProjectsStorage.Sprints.Add(sprint);

                        ProjectsStorage.SelectedSprint = sprint;


                        var tasks = createSprintWindow.GetSelectedTasks();

                        foreach (var task in tasks)
                        {
                            _mediator.Send(new AddInSprintProjectTaskCommand { ProjectTaskId = task.ProjectTaskId, SprintId = sprint.SprintId });

                            task.Status = (int)StatusProjectTaskEnum.TODO;
                            sprint.Tasks.Add(task);

                            ProjectsStorage.AddTasks(task);
                        }

                    }
                    catch
                    {
                        ErrorWindow.Show("Create sprint error");
                    }

                }
            }
        }
    }
}
