using MediatR;
using System.Windows.Controls;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.AddInSprintProjectTask;
using TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Modules.Projects.Storage;
using TeamTaskClient.UI.Modules.Projects.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectTasksPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        private static IMediator _mediator;
        //    private static ProjectModel _project;

        public ProjectPage(IMediator mediator)
        {
            _mediator = mediator;
            //  _project = project;

            InitializeComponent();
            DataContext = ProjectPageVM.GetInstance(mediator);

            ToBacklogButton.IsChecked = true;

        }

        private void ToBacklog(object sender, System.Windows.RoutedEventArgs e)
        {
            frameLayuot.NavigationService.Navigate(new BacklogProjectPage(_mediator));

        }

        private void ToSprints(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProjectsStorage.Sprints.Count > 0)
                frameLayuot.NavigationService.Navigate(new TasksPage(_mediator));
            else
            {
                if (ProjectsStorage.SelectedProject.UserRole != (int)UserRole.LEAD)
                {
                    ErrorWindow.Show("There are no sprints");
                }
                else
                {
                    CreateSprintWindow createSprintWindow = new CreateSprintWindow();
                    if (createSprintWindow.ShowDialog().Value)
                    {
                        var newSprint = createSprintWindow.GetSprintModel();
                        try
                        {

                            var sprint = _mediator.Send(new CreateSprintCommand
                            {
                                SprintEntity = new Domain.Entities.SprintEntity
                                {
                                    DateEnd = newSprint.DateEnd,
                                    DateStart = newSprint.DateStart,
                                    ProjectId = ProjectsStorage.SelectedProject.ProjectId,
                                }
                            }).Result;

                            ProjectsStorage.Sprints.Add(sprint);
                            ProjectsStorage.SelectedSprint = sprint;
                            var tasks = createSprintWindow.GetSelectedTasks();

                            foreach (var task in tasks)
                            {
                                _mediator.Send(new AddInSprintProjectTaskCommand { ProjectTaskId = task.ProjectTaskId, SprintId = sprint.SprintId });

                                ProjectsStorage.Sprints.First(s => s.SprintId == sprint.SprintId).Tasks.Add(task);
                            }

                            frameLayuot.NavigationService.Navigate(new TasksPage(_mediator));

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
