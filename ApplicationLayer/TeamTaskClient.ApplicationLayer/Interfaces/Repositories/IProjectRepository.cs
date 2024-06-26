﻿using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectModel>> GetProjectsByUserId(int id);
        Task CreateProject(int userId, string name);
        Task DeleteProject(int projectId);
        Task LeaveFromProject(int projectId, int userId);
        Task AddUserInProject(int projectId, string userTag);
        Task AddTeamInProject(int projectId, string teamTag);
        Task UpdateProject(ProjectEntity project);
        Task DeleteUserFromProject(int projectId, string userTag);

    }
}
