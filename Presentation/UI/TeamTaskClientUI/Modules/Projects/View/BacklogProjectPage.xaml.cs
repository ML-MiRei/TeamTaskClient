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
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.UI.UserControls;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для BacklogProjectPage.xaml
    /// </summary>
    public partial class BacklogProjectPage : Page
    {

        BacklogProjectPageVM vm;
        IProjectsCash _projectsCash;

        public BacklogProjectPage(IMediator mediator, IProjectsCash projectsCash)
        {
            InitializeComponent();
            vm = new BacklogProjectPageVM(mediator, projectsCash);
            DataContext = vm;
            _projectsCash = projectsCash;

            projectsCash.TaskChanged += OnTasksRefresh;
        }

        private void OnTasksRefresh(object? sender, EventArgs e)
        {
            TasksStories.Items.Refresh();
        }

        private void UsersProject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_projectsCash.SelectedProject.UserRole == (int)UserRoleEnum.LEAD)
            {
                vm.ChangeTask((ProjectTaskModel)((ProjectTaskTemplate)sender).DataContext);
            }
        }

        private void ProjectTaskTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ProjectTaskTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void UserTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ( ((UserModel)((UserTemplate)sender).DataContext).UserTag != Properties.Settings.Default.userTag)
            {
                vm.ActionWithUser((UserModel)((UserTemplate)sender).DataContext);
            }
        }

        private void UserTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((UserModel)((UserTemplate)sender).DataContext).UserTag != Properties.Settings.Default.userTag)
            {
                Cursor = Cursors.Hand;
            }
        }
    }
}
