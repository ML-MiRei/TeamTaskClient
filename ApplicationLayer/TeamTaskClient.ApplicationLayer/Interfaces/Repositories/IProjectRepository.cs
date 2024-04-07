using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectModel>> GetProjectsByUserId(int id);
        Task<ProjectModel> CreateProject(string name);
        Task DeleteProject(int projectId);
        Task LeaveFromProject(int projectId);
        Task UpdateProject(ProjectEntity project);
        Task DeleteUserFromProject(int projectId, string userTag);

    }
}
