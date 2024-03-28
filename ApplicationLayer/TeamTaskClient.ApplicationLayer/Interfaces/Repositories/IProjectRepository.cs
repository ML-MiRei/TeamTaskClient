using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectEntity>> GetProjectsByUserId(int id);
        Task<List<ProjectEntity>> GetProjectsByTeamId(int id);
        Task<ProjectEntity> CreateProject(ProjectDTO project);
        Task DeleteProject(int id);
        Task UpdateProject(ProjectDTO project);
        Task DeleteUserFromProject(int projectId, string userTag);
        Task LeaveFromProject(int projectId, int userId);
        Task DeleteTeamFromProject(int projectId, string teamTag);

    }
}
