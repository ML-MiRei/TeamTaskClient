using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.ChangeStatusProjectTask;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        IMediator _mediator;
        ProjectModel _projectModel;

        public TasksPage(IMediator mediator)
        {
            InitializeComponent();
            DataContext = new TasksVM(mediator);

            _mediator = mediator;

            ProjectsStorage.SelectedSprintChanged += OnSelectedSprintChanged;
        }

        private void OnSelectedSprintChanged(object? sender, SprintModel e)
        {
            DataContext = new TasksVM(_mediator);
        }

        private async void TaskDropDone(object sender, DragEventArgs e)
        {
            ProjectTaskModel projectTask = (ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]);

            try
            {
                projectTask.Status = (int)StatusProjectTaskEnum.DONE;

                await _mediator.Send(new ChangeStatusProjectTaskCommand
                {
                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
                    ProjectTaskId = projectTask.ProjectTaskId,
                    Status = projectTask.Status
                });

                ProjectsStorage.ChangeStatusProjectTask(projectTask);
            }
            catch
            {
                ErrorWindow.Show("Error change status");
            }

        }
        private async void TaskDropInProgress(object sender, DragEventArgs e)
        {
            ProjectTaskModel projectTask = (ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]);

            try
            {
                projectTask.Status = (int)StatusProjectTaskEnum.IN_PROGRESS;

                await _mediator.Send(new ChangeStatusProjectTaskCommand
                {
                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
                    ProjectTaskId = projectTask.ProjectTaskId,
                    Status = projectTask.Status
                });

                ProjectsStorage.ChangeStatusProjectTask(projectTask);
            }
            catch
            {
                ErrorWindow.Show("Error change status");
            }
        }
        private async void TaskDropTesting(object sender, DragEventArgs e)
        {
            ProjectTaskModel projectTask = (ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]);

            try
            {
                projectTask.Status = (int)StatusProjectTaskEnum.TESTING;

                await _mediator.Send(new ChangeStatusProjectTaskCommand
                {
                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
                    ProjectTaskId = projectTask.ProjectTaskId,
                    Status = projectTask.Status
                });

                ProjectsStorage.ChangeStatusProjectTask(projectTask);
            }
            catch
            {
                ErrorWindow.Show("Error change status");
            }


        }
        private async void TaskDropTodo(object sender, DragEventArgs e)
        {
            ProjectTaskModel projectTask = (ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]);

            try
            {
                projectTask.Status = (int)StatusProjectTaskEnum.TODO;

                await _mediator.Send(new ChangeStatusProjectTaskCommand
                {
                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
                    ProjectTaskId = projectTask.ProjectTaskId,
                    Status = projectTask.Status
                });

                ProjectsStorage.ChangeStatusProjectTask(projectTask);
            }
            catch
            {
                ErrorWindow.Show("Error change status");
            }
        }

        private void ProjectTaskTemplate_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ProjectTaskTemplate projectTaskTemplate = (ProjectTaskTemplate)sender;


            if ((ProjectsStorage.SelectedSprint.DateEnd.Date > DateTime.Now.Date) && (((ProjectTaskModel)projectTaskTemplate.DataContext).IsUserExecutor || ProjectsStorage.SelectedProject.UserRole == (int)UserRole.LEAD))
                DragDrop.DoDragDrop(projectTaskTemplate, projectTaskTemplate.DataContext, DragDropEffects.Move);
        }

        private void ProjectTaskTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            ProjectTaskTemplate projectTaskTemplate = (ProjectTaskTemplate)sender;
            if (ProjectsStorage.SelectedSprint.DateEnd.Date > DateTime.Now.Date && (((ProjectTaskModel)projectTaskTemplate.DataContext).IsUserExecutor || ProjectsStorage.SelectedProject.UserRole == (int)UserRole.LEAD))
                Cursor = Cursors.Hand;
        }

        private void ProjectTaskTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
