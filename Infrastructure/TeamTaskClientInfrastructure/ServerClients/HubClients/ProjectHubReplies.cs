using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class ProjectHubReplies : IProjectsEvents
    {
        private HubConnection _hubConnection;


        public ProjectHubReplies(IProjectHubConnection projectHubConnection, int userId, string userTag)
        {
            _hubConnection = projectHubConnection.HubConnection;


            _hubConnection.SendAsync("ConnectUserWithProjects", userId, userTag).Wait();


            _hubConnection.On<ProjectModel>("NewProjectReply", (projectModel) =>
            {
                NewProjectAdded?.Invoke(null, projectModel);
            });


            _hubConnection.On<int, UserModel>("AddUserInProjectReply", (projectId, userModel) =>
            {
                UserInProjectAdded?.Invoke(projectId, userModel);
            });


            _hubConnection.On<int>("DeleteProjectReply", (projectId) =>
            {
                ProjectDeleted?.Invoke(null, projectId);
            });


            _hubConnection.On<int, string>("DeleteUserFromProjectReply", (projectId, userTag) =>
            {
                UserFromProjectDeleted?.Invoke(projectId, userTag);
            });

            _hubConnection.On<ProjectEntity>("UpdateProjectReply", (projectModel) =>
            {
                ProjectUpdated?.Invoke(null, projectModel);
            });


            _hubConnection.On<ProjectTaskEntity>("UpdateProjectTaskReply", (projectTaskEntity) =>
            {
                ProjectTaskUpdated?.Invoke(null, projectTaskEntity);
            });


            _hubConnection.On<ProjectTaskEntity>("ChangeStatusProjectTaskReply", (projectTaskEntity) =>
            {
                StatusProjectTaskChanged?.Invoke(null, projectTaskEntity);
            });



            _hubConnection.On<ProjectTaskEntity>("SetExecutorProjectTaskReply", (projectTaskEntity) =>
            {
                ExecuterProjectTaskChanged?.Invoke(null, projectTaskEntity);
            });



            _hubConnection.On<int, int>("DeleteProjectTaskReply", (projectId, projectTaskId) =>
            {
                ProjectTaskDeleted?.Invoke(projectId, projectTaskId);
            });


            _hubConnection.On<ProjectTaskEntity>("AddNewProjectTaskReply", (projectTaskEntity) =>
            {
                ProjectTaskCreated?.Invoke(null, projectTaskEntity);
            });



            _hubConnection.On<SprintEntity>("UpdateDateStartSprintReply", (sprintEntity) =>
            {
                DateStartSprintUpdated?.Invoke(null, sprintEntity);
            });


            _hubConnection.On<SprintEntity>("UpdateDateEndSprintReply", (sprintEntity) =>
            {
                DateEndSprintUpdated?.Invoke(null, sprintEntity);
            });


            _hubConnection.On<int, SprintModel>("CreateSprintReply", (projectId, sprintModel) =>
            {
                SprintCreated?.Invoke(projectId, sprintModel);
            });


            _hubConnection.On<int, int>("DeleteSprintReply", (projectId, sprintId) =>
            {
                SprintDeleted?.Invoke(projectId, sprintId);
            });


            _hubConnection.On<NotificationModel>("NotificationReply", (notificationModel) =>
            {
                NotificationAdded?.Invoke(null, notificationModel);
            });

        }


        public event EventHandler<ProjectModel> NewProjectAdded;
        public event EventHandler<ProjectEntity> ProjectUpdated;
        public event EventHandler<int> ProjectDeleted;
        public event EventHandler<UserModel> UserInProjectAdded;
        public event EventHandler<string> UserFromProjectDeleted;


        public event EventHandler<ProjectTaskEntity> ProjectTaskUpdated;
        public event EventHandler<ProjectTaskEntity> ProjectTaskCreated;
        public event EventHandler<int> ProjectTaskDeleted;
        public event EventHandler<ProjectTaskEntity> StatusProjectTaskChanged;
        public event EventHandler<ProjectTaskEntity> ExecuterProjectTaskChanged;


        public event EventHandler<SprintEntity> DateStartSprintUpdated;
        public event EventHandler<SprintEntity> DateEndSprintUpdated;
        public event EventHandler<SprintModel> SprintCreated;
        public event EventHandler<int> SprintDeleted;

        public event EventHandler<NotificationModel> NotificationAdded;

    }
}
