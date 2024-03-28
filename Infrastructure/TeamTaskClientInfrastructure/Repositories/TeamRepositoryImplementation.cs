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
    public class TeamRepositoryImplementation : ITeamRepository
    {
        public Task AddUserInTeamByTag(int teamId, string userTag)
        {
            throw new NotImplementedException();
        }

        public Task<TeamEntity> CreateTeam(TeamDTO team)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task DeletUserFromTeam(string userTag, int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamEntity>> GetTeamsByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamEntity>> GetTeamsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task LeaveTeam(int userId, int teamId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTeam(TeamDTO team)
        {
            throw new NotImplementedException();
        }
    }
}
