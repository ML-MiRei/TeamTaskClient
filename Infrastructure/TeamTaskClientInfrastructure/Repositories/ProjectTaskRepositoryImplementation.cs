using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectTaskRepositoryImplementation : IProjectTaskRepository
    {
        public Task ChangeStatusProjectTask(int projectId, int projectTaskId, StatusProjectTaskEnum status)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTaskEntity> CreateProjectTask(ProjectTaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjectTask(int projectId, int projectTaskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectTaskEntity>> GetProjectTasksByProjectId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectTaskEntity>> GetProjectTasksByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectTask(ProjectTaskDTO projectTask)
        {
            throw new NotImplementedException();
        }
    }
}
