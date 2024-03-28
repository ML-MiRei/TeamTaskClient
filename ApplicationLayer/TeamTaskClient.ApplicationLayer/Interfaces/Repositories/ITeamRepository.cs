using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface ITeamRepository
    {
        Task<List<TeamEntity>> GetTeamsByUserId(int userId);
        Task<List<TeamEntity>> GetTeamsByProjectId(int projectId);
        Task<TeamEntity> CreateTeam(TeamDTO team);
        Task UpdateTeam(TeamDTO team);
        Task DeleteTeam(int teamId);
        Task LeaveTeam(int userId, int teamId);
        Task DeletUserFromTeam(string userTag, int teamId);
        Task AddUserInTeamByTag(int teamId, string userTag);
    }
}
