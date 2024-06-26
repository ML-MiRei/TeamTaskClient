﻿using MediatR;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class ObserveProjectsPage : Page
    {
        private static ObserverProjectPageVM vm;
        private static IProjectsCash _projectsCash;

        public ObserveProjectsPage(IMediator mediator, IProjectsCash projectsCash)
        {
            InitializeComponent();
            _projectsCash = projectsCash;

            vm = new ObserverProjectPageVM(mediator, _projectsCash);
            DataContext = vm;


            _projectsCash.SelectedProjectChanged += OnSelectedProjectChanged;

        }

        private void OnSelectedProjectChanged(object? sender, ProjectModel e)
        {
            App.Current.Dispatcher.Invoke(() => ListProjects.Items.Refresh());
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListProjects.ItemsSource = _projectsCash.Projects.Where(p => p.ProjectName.Contains(vm.InputSearchString.Trim()));
        }

        private void ProjectTemplate_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            _projectsCash.SelectedProject = (((ProjectTemplate)sender).DataContext as ProjectModel);


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
