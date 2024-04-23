using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteProject;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.LeaveProject;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Queries.GetProjectsByUserId;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Teams.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class ObserverProjectPageVM : ViewModelBase
    {
        private static IMediator _mediator;
        public string WatermarkText { get => "Project name.."; }

        public ObserverProjectPageVM(IMediator mediator)
        {
            _mediator = mediator;
            InputSearchString = WatermarkText;

            ProjectsStorage.Projects = new ObservableCollection<ProjectModel>(mediator.Send(new GetProjectsByUserIdQuery { UserId = Properties.Settings.Default.userId }).Result);

            CreateProject = new NewProjectCommand(this);

        }

        public ObservableCollection<ProjectModel> Projects
        {
            get { return ProjectsStorage.Projects; }
        }


        private string _inputSearchString;
        public string InputSearchString
        {
            get { return _inputSearchString; }
            set
            {
                _inputSearchString = value;
                OnPropertyChanged(nameof(InputSearchString));
            }
        }


        public async void DeleteProject(ProjectModel projectModel)
        {
            if (projectModel.UserRole == (int)UserRole.LEAD)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Delete" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                    if (alertDialogWindow.ShowDialog().Value)
                    {
                        try
                        {
                            await _mediator.Send(new DeleteProjectCommand { ProjectId = projectModel.ProjectId });

                        }
                        catch (Exception)
                        {
                            ErrorWindow.Show("Error project delete");
                        }
                    }

                }

            }
            else
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string> { "Leave" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Delete", "Cancel");
                    if (alertDialogWindow.ShowDialog().Value)
                    {
                        try
                        {
                            await _mediator.Send(new LeaveProjectCommand { ProjectId = projectModel.ProjectId });

                        }
                        catch (Exception)
                        {
                            ErrorWindow.Show("Error project leave");
                        }
                    }

                }
            }
            ProjectsStorage.Projects.Remove(projectModel);

        }


        public ICommand SearchProject { get; }
        public ICommand ViewButton { get; }
        public ICommand CreateProject { get; }


        private class NewProjectCommand(ObserverProjectPageVM vM) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                CreateSubjectDialogWindow dialogWindow = new CreateSubjectDialogWindow("Creating a project", new List<string> { "Project name:" });
                if (dialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        var project = _mediator.Send(new CreateProjectCommand { ProjectName = (dialogWindow.GetCreatingProperties()[0]) }).Result;

                        vM.Projects.Add(project);
                    }
                    catch
                    {
                        ErrorWindow.Show("Error creating the project");
                    }

                }
            }
        }
    }
}
