using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.CreateProject;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.DeleteProject;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.LeaveProject;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Queries.GetProjectsByUserId;
using TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.CreateTeam;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Teams.ViewModels;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class ObserverProjectPageVM : ViewModelBase
    {
        private static IMediator _mediator;
        private static IProjectsCash _projectsCash;
        public string WatermarkText { get => "Project name.."; }

        public ObserverProjectPageVM(IMediator mediator, IProjectsCash projectsCash)
        {
            _mediator = mediator;
            _projectsCash = projectsCash;

            InputSearchString = WatermarkText;

            _projectsCash.Projects = new ObservableCollection<ProjectModel>(mediator.Send(new GetProjectsByUserIdQuery { UserId = Properties.Settings.Default.userId }).Result);

            CreateProject = new NewProjectCommand(this);

            _projectsCash.ProjectChanged += OnProjectChanged;

        }

        private void OnProjectChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Projects));
        }

        public ObservableCollection<ProjectModel> Projects
        {
            get { return _projectsCash.Projects; }
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
            if (projectModel.UserRole == (int)UserRoleEnum.LEAD)
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
                            await _mediator.Send(new LeaveProjectCommand { ProjectId = projectModel.ProjectId, UserId = Properties.Settings.Default.userId });

                        }
                        catch (Exception)
                        {
                            ErrorWindow.Show("Error project leave");
                        }
                    }

                }
            }
        }


        public ICommand SearchProject { get; }
        public ICommand ViewButton { get; }
        public ICommand CreateProject { get; }


        private class NewProjectCommand(ObserverProjectPageVM vM) : CommandBase
        {
            public override async void Execute(object? parameter)
            {
                CreateSubjectDialogWindow dialogWindow = new CreateSubjectDialogWindow("Creating a project", new List<string> { "Project name:" });
                if (dialogWindow.ShowDialog().Value)
                {
                    try
                    {
                        await _mediator.Send(new CreateProjectCommand { ProjectName = (dialogWindow.GetCreatingProperties()[0]), UserId = Properties.Settings.Default.userId });
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
