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
    public interface ITeamRepository
    {
        Task<List<TeamModel>> GetTeamsByUserId(int userId);
        Task<TeamModel> CreateTeam(TeamEntity team);
        Task UpdateTeam(TeamEntity team);
        Task LeaveTeam(int userId, int teamId);
        Task DeleteUserFromTeam(string userTag, int teamId);
        Task AddUserInTeamByTag(int teamId, string userTag);
    }
}
