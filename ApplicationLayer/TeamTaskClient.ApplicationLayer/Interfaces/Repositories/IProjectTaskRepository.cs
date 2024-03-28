using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<List<ProjectTaskEntity>> GetProjectTasksByProjectId(int id);
        Task<List<ProjectTaskEntity>> GetProjectTasksByUserId(int id);
        Task<ProjectTaskEntity> CreateProjectTask(ProjectTaskDTO entity);
        Task UpdateProjectTask(ProjectTaskDTO projectTask);
        Task DeleteProjectTask(int projectId, int projectTaskId);
        Task ChangeStatusProjectTask(int projectId, int projectTaskId, StatusProjectTaskEnum status);

    }
}
