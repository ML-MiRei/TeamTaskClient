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
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Modules.Projects.Storage;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;
using TeamTaskClient.UI.Modules.Teams.UserControls;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для BacklogProjectPage.xaml
    /// </summary>
    public partial class BacklogProjectPage : Page
    {

        BacklogProjectPageVM vm;

        public BacklogProjectPage(IMediator mediator)
        {
            InitializeComponent();
            vm = new BacklogProjectPageVM(mediator);
            DataContext = vm;

        }

        private void UsersProject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ProjectsStorage.SelectedProject.UserRole == (int)UserRole.LEAD)
            {
                vm.ChangeTask((ProjectTaskModel)((ProjectTaskTemplate)sender).DataContext);
            }
        }

        private void ProjectTaskTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ProjectsStorage.SelectedProject.UserRole == (int)UserRole.LEAD)
            {
                Cursor = Cursors.Hand;
            }
        }

        private void ProjectTaskTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void UserTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ProjectsStorage.SelectedProject.UserRole == (int)UserRole.LEAD)
            {
                vm.ActionWithUser((UserModel)((UserTemplate)sender).DataContext);
            }
        }
    }
}
