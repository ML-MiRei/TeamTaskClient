using MediatR;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.ChangeStatusProjectTask;
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.CreateProjectTask;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClientUI.Main;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.DeleteSprint;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.ChangeDateEndSprint;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.ChangeDateStartSprint;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class TasksVM : ViewModelBase
    {
        private static IMediator _mediator;
        private static IProjectsCash _projectsCash;


        public TasksVM(IMediator mediator, IProjectsCash projectsCash)
        {
            _mediator = mediator;
            _projectsCash = projectsCash;

            SelectSprint = new SelectSprintCommand(this);
            EditSprint = new EditSprintCommand(this);

            projectsCash.TaskChanged += OnTasksRefresh;
            projectsCash.SprintChanged += OnSprintChanged;
        }

        private void OnSprintChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(SprintNumber));
            OnPropertyChanged(nameof(DateEnd));
            OnPropertyChanged(nameof(DateStart));
        }

        private void OnTasksRefresh(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ProjectTasksDone));
            OnPropertyChanged(nameof(ProjectTasksInProcess));
            OnPropertyChanged(nameof(ProjectTasksTesting));
            OnPropertyChanged(nameof(ProjectTasksTodo));
        }

     
        public async void StatusProjectTaskChange(ProjectTaskModel projectTaskModel, StatusProjectTaskEnum newStatus)
        {

            try
            {
                await _mediator.Send(new ChangeStatusProjectTaskCommand
                {
                    ProjectId = _projectsCash.SelectedProject.ProjectId,
                    ProjectTaskId = projectTaskModel.ProjectTaskId,
                    Status = (int)newStatus,
                    SprintId = _projectsCash.SelectedSprint.SprintId
                });
            }
            catch
            {
                ErrorWindow.Show("Error change status");
            }

        }

        public int UserRole => _projectsCash.SelectedProject.UserRole;


        public string SprintNumber { get => (_projectsCash.Sprints.IndexOf(_projectsCash.SelectedSprint) + 1).ToString(); }


        public ObservableCollection<ProjectTaskModel> ProjectTasksTodo
        {
            get { return new ObservableCollection<ProjectTaskModel>(_projectsCash.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.TODO)); }
        }

        public ObservableCollection<ProjectTaskModel> ProjectTasksInProcess
        {
            get { return new ObservableCollection<ProjectTaskModel>(_projectsCash.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.IN_PROGRESS)); }
        }

        public ObservableCollection<ProjectTaskModel> ProjectTasksTesting
        {
            get { return new ObservableCollection<ProjectTaskModel>(_projectsCash.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.TESTING)); }
        }



        public ObservableCollection<ProjectTaskModel> ProjectTasksDone
        {
            get { return new ObservableCollection<ProjectTaskModel>(_projectsCash.Tasks.Where(t => t.Status == (int)StatusProjectTaskEnum.DONE)); }
        }



        public string DateStart => _projectsCash.SelectedSprint.DateStart.ToString("d");
        public string DateEnd => _projectsCash.SelectedSprint.DateEnd.ToString("d");


        public ICommand AddSprint { get; } = new AddSprintCommand();
        public ICommand SelectSprint { get; }
        public ICommand EditSprint { get; }

        private class EditSprintCommand(TasksVM vm) : CommandBase
        {
            public override async void Execute(object? parameter)
            {



                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select sprint", new List<string> { "Change date start", "Change date end", "Delete" });

                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    var action = selectActionsDialogWindow.GetSelectedAction();

                    switch (action)
                    {
                        case "Change date start":

                            ChangeDateWindow changeDateWindow = new ChangeDateWindow(_projectsCash.SelectedSprint.DateStart);
                            if (changeDateWindow.ShowDialog().Value)
                            {
                                await _mediator.Send(new ChangeDateStartSprintCommand
                                {
                                    DateStart = changeDateWindow.SelectedDate,
                                    ProjectId = _projectsCash.SelectedProject.ProjectId,
                                    SprintId = _projectsCash.SelectedSprint.SprintId
                                });
                            }


                            break;

                        case "Change date end":

                            ChangeDateWindow changeDateEndWindow = new ChangeDateWindow(_projectsCash.SelectedSprint.DateEnd);
                            if (changeDateEndWindow.ShowDialog().Value)
                            {
                                await _mediator.Send(new ChangeDateEndSprintCommand
                                {
                                    DateEnd = changeDateEndWindow.SelectedDate,
                                    ProjectId = _projectsCash.SelectedProject.ProjectId,
                                    SprintId = _projectsCash.SelectedSprint.SprintId
                                });
                            }


                            break;

                        case "Delete":

                            AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                            if (alertDialogWindow.ShowDialog().Value)
                            {
                                _mediator.Send(new DeleteSprintCommand { ProjectId = _projectsCash.SelectedProject.ProjectId, SprintId = _projectsCash.SelectedSprint.SprintId });
                            }
                            break;
                    }

                }


            }
        }


        private class SelectSprintCommand(TasksVM vm) : CommandBase
        {
            public override void Execute(object? parameter)
            {

                List<string> list = new List<string>();

                for (int i = 0; i < _projectsCash.Sprints.Count; i++)
                {
                    list.Add($"Sprint №{i + 1},    {_projectsCash.Sprints[i].DateStart.ToString("d")} - {_projectsCash.Sprints[i].DateEnd.ToString("d")}");
                }


                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select sprint", list);

                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    var selectedSprint = _projectsCash.Sprints[list.IndexOf(selectActionsDialogWindow.GetSelectedAction())];
                    _projectsCash.SelectedSprint = selectedSprint;

                }


            }
        }

        private class AddSprintCommand : CommandBase
        {
            public async override void Execute(object? parameter)
            {
                CreateSprintWindow createSprintWindow = new CreateSprintWindow(_projectsCash);
                if (createSprintWindow.ShowDialog().Value)
                {

                    var newSprint = createSprintWindow.GetSprintModel();
                    try
                    {

                        await _mediator.Send(new CreateSprintCommand
                        {
                            SprintModel = new SprintModel
                            {
                                DateEnd = newSprint.DateEnd,
                                DateStart = newSprint.DateStart,
                                Tasks = createSprintWindow.GetSelectedTasks()
                            },
                            ProjectId = _projectsCash.SelectedProject.ProjectId

                        });

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
