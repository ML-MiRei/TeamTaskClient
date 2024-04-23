using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.UpdateProject;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.View;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    public class ProjectPageVM : ViewModelBase
    {

        private static IMediator _mediator;
        private static ProjectPageVM _instance;




        public static ProjectPageVM GetInstance(IMediator mediator)
        {
            if (_instance == null)
                _instance = new ProjectPageVM(mediator);
            return _instance;
        }


        private ProjectPageVM(IMediator mediator)
        {
            _mediator = mediator;

            EditProjectName = new EditProjectNameCommand(this);
            ProjectsStorage.SelectedProjectChanged += OnSelectedProjectChanged;
        }

        private void OnSelectedProjectChanged(object? sender, ProjectModel e)
        {
            OnPropertyChanged(nameof(ProjectName));
        }


        public int UserRole => ProjectsStorage.SelectedProject.UserRole;

        public string ProjectName { get => ProjectsStorage.SelectedProject.ProjectName; }


        public ICommand EditProjectName { get; } 

        private class EditProjectNameCommand(ProjectPageVM vm) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                InputDialogWindow inputDialog = new InputDialogWindow("Edit project", "Save", new List<string> { ProjectsStorage.SelectedProject.ProjectName });
                if (inputDialog.ShowDialog().Value)
                {
                    try
                    {
                        _mediator.Send(new UpdateProjectCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId, ProjectName = inputDialog.GetInputValue()[0] });
                        ProjectsStorage.SelectedProject.ProjectName = inputDialog.GetInputValue()[0];
                        ProjectsStorage.Projects.First(p => p.ProjectId == ProjectsStorage.SelectedProject.ProjectId).ProjectName = inputDialog.GetInputValue()[0];
                        vm.OnPropertyChanged(nameof(ProjectName));
                    }
                    catch
                    {
                        ErrorWindow.Show("Edit project error");


                    }
                }
            }
        }


    }
}
