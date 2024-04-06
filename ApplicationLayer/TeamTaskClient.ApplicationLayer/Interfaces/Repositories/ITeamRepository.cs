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
        Task<TeamModel> CreateTeam(TeamDTO team);
        Task UpdateTeam(TeamDTO team);
        Task LeaveTeam(int userId, int teamId);
        Task DeleteUserFromTeam(string userTag, string teamTag);
        Task AddUserInTeamByTag(string teamTag, string userTag);
    }
}
