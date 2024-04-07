using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
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
        Task ChangeStatusProjectTask(int projectTaskId, StatusProjectTaskEnum status);

    }
}
