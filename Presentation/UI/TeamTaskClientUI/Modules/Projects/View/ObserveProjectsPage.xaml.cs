using MediatR;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class ObserveProjectsPage : Page
    {
        ObserverProjectPageVM vm;

        public ObserveProjectsPage(IMediator mediator)
        {
            InitializeComponent();

            vm = new ObserverProjectPageVM(mediator);
            DataContext = vm;


            ProjectsStorage.SelectedProjectChanged += OnSelectedProjectChanged;

        }

        private void OnSelectedProjectChanged(object? sender, ProjectModel e)
        {
            ListProjects.Items.Refresh();
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListProjects.ItemsSource = ProjectsStorage.Projects.Where(p => p.ProjectName.Contains(vm.InputSearchString.Trim()));
        }

        private void ProjectTemplate_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            ProjectsStorage.SelectedProject = (((ProjectTemplate)sender).DataContext as ProjectModel);


            MainWindowVM.ToProjectTaskButton();
        }

        private void ProjectTemplate_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            var projectModel = (((ProjectTemplate)sender).DataContext as ProjectModel);

                vm.DeleteProject(projectModel);

        }

        private void ProjectTemplate_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ProjectTemplate_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
