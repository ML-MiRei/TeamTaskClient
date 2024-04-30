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
using TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.ChangeStatusProjectTask;
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

        TasksVM vm;

        public TasksPage(IMediator mediator)
        {
            InitializeComponent();
            vm = new TasksVM(mediator);
            DataContext = vm;

            _mediator = mediator;

            ProjectsStorage.SelectedSprintChanged += OnSelectedSprintChanged;
        }



        private void OnSelectedSprintChanged(object? sender, SprintModel e)
        {
            if (e != null)
                DataContext = new TasksVM(_mediator);
            else
                ProjectPage.Instance.ToBacklogButton.IsChecked = true;
        }

        private async void TaskDropDone(object sender, DragEventArgs e)
        {
            vm.StatusProjectTaskChange((ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]), StatusProjectTaskEnum.DONE);
        }

        private async void TaskDropInProgress(object sender, DragEventArgs e)
        {
            vm.StatusProjectTaskChange((ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]), StatusProjectTaskEnum.IN_PROGRESS);
        }

        private async void TaskDropTesting(object sender, DragEventArgs e)
        {
            vm.StatusProjectTaskChange((ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]), StatusProjectTaskEnum.TESTING);
        }

        private async void TaskDropTodo(object sender, DragEventArgs e)
        {
            vm.StatusProjectTaskChange((ProjectTaskModel)e.Data.GetData(e.Data.GetFormats()[0]), StatusProjectTaskEnum.TODO);
        }

        private void ProjectTaskTemplate_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ProjectTaskTemplate projectTaskTemplate = (ProjectTaskTemplate)sender;


            if ((ProjectsStorage.SelectedSprint.DateEnd.Date > DateTime.Now.Date) &&
                (((ProjectTaskModel)projectTaskTemplate.DataContext).ExecutorTag == Properties.Settings.Default.userTag ||
                ProjectsStorage.SelectedProject.UserRole == (int)UserRoleEnum.LEAD))
                DragDrop.DoDragDrop(projectTaskTemplate, projectTaskTemplate.DataContext, DragDropEffects.Move);
        }

        private void ProjectTaskTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            ProjectTaskTemplate projectTaskTemplate = (ProjectTaskTemplate)sender;
            if ((ProjectsStorage.SelectedSprint.DateEnd.Date > DateTime.Now.Date) &&
                          (((ProjectTaskModel)projectTaskTemplate.DataContext).ExecutorTag == Properties.Settings.Default.userTag ||
                          ProjectsStorage.SelectedProject.UserRole == (int)UserRoleEnum.LEAD))
                Cursor = Cursors.Hand;
        }

        private void ProjectTaskTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
