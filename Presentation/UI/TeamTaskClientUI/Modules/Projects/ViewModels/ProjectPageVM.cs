using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.UpdateProject;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.View;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChatByProject;

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
            public async override void Execute(object? parameter)
            {
                SelectActionsDialogWindow selectActionsDialogWindow = new SelectActionsDialogWindow("Select action", new List<string>() { "Change name", "Create chat" });
                if (selectActionsDialogWindow.ShowDialog().Value)
                {
                    switch (selectActionsDialogWindow.GetSelectedAction())
                    {
                        case "Change name":

                            InputDialogWindow inputDialog = new InputDialogWindow("Edit project", "Save", new List<string> { ProjectsStorage.SelectedProject.ProjectName });
                            if (inputDialog.ShowDialog().Value)
                            {
                                try
                                {
                                    await _mediator.Send(new UpdateProjectCommand { ProjectId = ProjectsStorage.SelectedProject.ProjectId, ProjectName = inputDialog.GetInputValue()[0] });
                                }
                                catch
                                {
                                    ErrorWindow.Show("Edit project error");


                                }
                            }
                            break;

                        case "Create chat":

                            AlertDialogWindow alertDialogWindow = new AlertDialogWindow("Are you sure?", "Create", "Cancel");
                            if (alertDialogWindow.ShowDialog().Value)
                            {
                                try
                                {
                                    await _mediator.Send(new CreateGroupChatByProjectCommand
                                    {
                                        UserId = Properties.Settings.Default.userId,
                                        ProjectId = ProjectsStorage.SelectedProject.ProjectId
                                    });
                                }
                                catch (Exception)
                                {
                                    ErrorWindow.Show("Error chat created");
                                }
                            }

                            break;
                    }


                }

            }
        }


    }
}
