using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {

        Task<ProjectTaskModel> CreateProjectTask(ProjectTaskEntity entity);
        Task UpdateProjectTask(ProjectTaskEntity projectTask);
        Task SetExecutorProjectTask(int projectTaskId, string userTag);
        Task DeleteProjectTask(int projectId, int projectTaskId);
        Task ChangeStatusProjectTask(int projectId, int projectTaskId, int status);
        Task AddInSprintProjectTask(int projectTaskId, int sprintId);

    }
}
