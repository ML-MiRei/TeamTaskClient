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


            HubClient.On<ProjectModel>("UpdateProjectReply", (projectModel) =>
            {
                ProjectUpdated?.Invoke(null, projectModel);
            });


            HubClient.On<ProjectTaskModel>("UpdateProjectTaskReply", (projectTaskModel) =>
            {
                ProjectTaskUpdated?.Invoke(null, projectTaskModel);
            });


            HubClient.On<ProjectTaskModel>("ChangeStatusProjectTaskReply", (projectTaskModel) =>
            {
                StatusProjectTaskChanged?.Invoke(null, projectTaskModel);
            });



            HubClient.On<ProjectTaskModel>("SetExecutorProjectTaskReply", (projectTaskModel) =>
            {
                StatusProjectTaskChanged?.Invoke(null, projectTaskModel);
            });



            HubClient.On<int>("DeleteProjectTaskReply", (projectTaskId) =>
            {
                ProjectTaskDeleted?.Invoke(null, projectTaskId);
            });


            HubClient.On<ProjectTaskModel>("AddNewProjectTaskReply", (projectTaskModel) =>
            {
                ProjectTaskCreated?.Invoke(null, projectTaskModel);
            });



            HubClient.On<SprintEntity>("UpdateDateStartSprintReply", (sprintEntity) =>
            {
                DateStartSprintUpdated?.Invoke(null, sprintEntity);
            });


            HubClient.On<SprintEntity>("UpdateDateEndSprint", (sprintEntity) =>
            {
                DateEndSprintUpdated?.Invoke(null, sprintEntity);
            });


            HubClient.On<SprintModel>("CreateSprintReply", (sprintModel) =>
            {
                SprintCreated?.Invoke(null, sprintModel); 
            });
            

            HubClient.On<int>("DeleteSprintReply", (sprintId) =>
            {
                SprintDeleted?.Invoke(null, sprintId);
            });



        }


        public static event EventHandler<ProjectModel> NewProjectAdded;
        public static event EventHandler<ProjectModel> ProjectUpdated;
        public static event EventHandler<int> ProjectDeleted;
        public static event EventHandler<UserModel> UserInProjectAdded;
        public static event EventHandler<string> UserFromProjectDeleted;

        public static event EventHandler<ProjectTaskModel> ProjectTaskUpdated;
        public static event EventHandler<ProjectTaskModel> ProjectTaskCreated;
        public static event EventHandler<int> ProjectTaskDeleted;
        public static event EventHandler<ProjectTaskModel> StatusProjectTaskChanged;
        public static event EventHandler<ProjectTaskModel> ExecuterProjectTaskChanged;


        public static event EventHandler<SprintEntity> DateStartSprintUpdated;
        public static event EventHandler<SprintEntity> DateEndSprintUpdated;
        public static event EventHandler<SprintModel> SprintCreated;
        public static event EventHandler<int> SprintDeleted;
    }
}
