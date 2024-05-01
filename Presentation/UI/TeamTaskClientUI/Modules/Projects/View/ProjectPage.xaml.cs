using MediatR;
using System.Windows.Controls;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Modules.Projects.ViewModels;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectTasksPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        private static IMediator _mediator;
        private static IProjectsCash _projectsCash;
        public static ProjectPage Instance { get; set; }


        public ProjectPage(IMediator mediator, IProjectsCash projectsCash)
        {
            _mediator = mediator;
            _projectsCash = projectsCash;

            InitializeComponent();
            DataContext = new ProjectPageVM(mediator, projectsCash);

            ToBacklogButton.IsChecked = true;
            Instance = this;

        }



        private void ToBacklog(object sender, System.Windows.RoutedEventArgs e)
        {
            frameLayuot.NavigationService.Navigate(new BacklogProjectPage(_mediator, _projectsCash));

        }

        private async void ToSprints(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_projectsCash.Sprints.Count > 0)
                frameLayuot.NavigationService.Navigate(new TasksPage(_mediator, _projectsCash));
            else
            {
                ToTasksButton.IsChecked = false;
                if (_projectsCash.SelectedProject.UserRole != (int)UserRoleEnum.LEAD)
                {
                    ErrorWindow.Show("There are no sprints");
                }
                else
                {
                    CreateSprintWindow createSprintWindow = new CreateSprintWindow(_projectsCash);
                    if (createSprintWindow.ShowDialog().Value)
                    {

                        var newSprint = createSprintWindow.GetSprintModel();
                        try
                        {

                            await _mediator.Send(new CreateSprintCommand
                            {
                                SprintModel = new SprintModel
                                {
                                    DateEnd = newSprint.DateEnd,
                                    DateStart = newSprint.DateStart,
                                    Tasks = createSprintWindow.GetSelectedTasks()
                                },
                                ProjectId = _projectsCash.SelectedProject.ProjectId,
                            });

                        }
                        catch
                        {
                            ErrorWindow.Show("Create sprint error");
                        }

                    }
                }
            }

        }
    }
}
