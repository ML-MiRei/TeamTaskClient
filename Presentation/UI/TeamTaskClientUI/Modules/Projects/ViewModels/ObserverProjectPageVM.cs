using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Queries.GetProjectsByUserId;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Projects.Storage;
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
