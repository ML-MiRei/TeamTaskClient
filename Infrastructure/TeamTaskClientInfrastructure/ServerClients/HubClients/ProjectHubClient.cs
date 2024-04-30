using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.HubClients
{
    public class ProjectHubClient
    {
        public HubConnection HubClient;


        private static ProjectHubClient _instance;
        public static ProjectHubClient GetInstance(int userId, string userTag)
        {

            if (_instance == null)
                _instance = new ProjectHubClient(userId, userTag);
            return _instance;

        }
        public static ProjectHubClient GetInstance()
        {
            return _instance;
        }


        private ProjectHubClient(int userId, string userTag)
        {
            HubClient = ProjectHubConnection.Instance.GetClient();


            HubClient.SendAsync("ConnectUserWithProjects", userId, userTag).Wait();


            HubClient.On<ProjectModel>("NewProjectReply", (projectModel) =>
            {
                NewProjectAdded?.Invoke(null, projectModel);
            });


            HubClient.On<int, UserModel>("AddUserInProjectReply", (projectId, userModel) =>
            {
                UserInProjectAdded?.Invoke(projectId, userModel);
            });


            HubClient.On<int>("DeleteProjectReply", (projectId) =>
            {
                ProjectDeleted?.Invoke(null, projectId);
            });


            HubClient.On<int, string>("DeleteUserFromProjectReply", (projectId, userTag) =>
            {
                UserFromProjectDeleted?.Invoke(projectId, userTag);
            });


            HubClient.On<ProjectEntity>("UpdateProjectReply", (projectModel) =>
            {
                ProjectUpdated?.Invoke(null, projectModel);
            });


            HubClient.On<ProjectTaskEntity>("UpdateProjectTaskReply", (projectTaskEntity) =>
            {
                ProjectTaskUpdated?.Invoke(null, projectTaskEntity);
            });


            HubClient.On<ProjectTaskEntity>("ChangeStatusProjectTaskReply", (projectTaskEntity) =>
            {
                StatusProjectTaskChanged?.Invoke(null, projectTaskEntity);
            });



            HubClient.On<ProjectTaskEntity>("SetExecutorProjectTaskReply", (projectTaskEntity) =>
            {
                ExecuterProjectTaskChanged?.Invoke(null, projectTaskEntity);
            });



            HubClient.On<int, int>("DeleteProjectTaskReply", (projectId, projectTaskId) =>
            {
                ProjectTaskDeleted?.Invoke(projectId, projectTaskId);
            });


            HubClient.On<ProjectTaskEntity>("AddNewProjectTaskReply", (projectTaskEntity) =>
            {
                ProjectTaskCreated?.Invoke(null, projectTaskEntity);
            });
            


            HubClient.On<SprintEntity>("UpdateDateStartSprintReply", (sprintEntity) =>
            {
                DateStartSprintUpdated?.Invoke(null, sprintEntity);
            });


            HubClient.On<SprintEntity>("UpdateDateEndSprintReply", (sprintEntity) =>
            {
                DateEndSprintUpdated?.Invoke(null, sprintEntity);
            });


            HubClient.On<int, SprintModel>("CreateSprintReply", (projectId, sprintModel) =>
            {
                SprintCreated?.Invoke(projectId, sprintModel); 
            });
            

            HubClient.On<int, int>("DeleteSprintReply", (projectId, sprintId) =>
            {
                SprintDeleted?.Invoke(projectId, sprintId);
            });


            HubClient.On<NotificationModel>("NotificationReply", (notificationModel) =>
            {
                NotificationAdded?.Invoke(null, notificationModel);
            });

        }


        public static event EventHandler<ProjectModel> NewProjectAdded;
        public static event EventHandler<ProjectEntity> ProjectUpdated;
        public static event EventHandler<int> ProjectDeleted;
        public static event EventHandler<UserModel> UserInProjectAdded;
        public static event EventHandler<string> UserFromProjectDeleted;


        public static event EventHandler<ProjectTaskEntity> ProjectTaskUpdated;
        public static event EventHandler<ProjectTaskEntity> ProjectTaskCreated;
        public static event EventHandler<int> ProjectTaskDeleted;
        public static event EventHandler<ProjectTaskEntity> StatusProjectTaskChanged;
        public static event EventHandler<ProjectTaskEntity> ExecuterProjectTaskChanged;


        public static event EventHandler<SprintEntity> DateStartSprintUpdated;
        public static event EventHandler<SprintEntity> DateEndSprintUpdated;
        public static event EventHandler<SprintModel> SprintCreated;
        public static event EventHandler<int> SprintDeleted;

        public static event EventHandler<NotificationModel> NotificationAdded;

    }
}
