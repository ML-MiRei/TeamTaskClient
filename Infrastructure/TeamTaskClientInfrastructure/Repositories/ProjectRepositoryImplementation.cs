using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class ProjectRepositoryImplementation : IProjectRepository
    {
        public Task<ProjectEntity> CreateProject(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeamFromProject(int projectId, string teamTag)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserFromProject(int projectId, string userTag)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectEntity>> GetProjectsByTeamId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectEntity>> GetProjectsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task LeaveFromProject(int projectId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProject(ProjectDTO project)
        {
            throw new NotImplementedException();
        }
    }
}
