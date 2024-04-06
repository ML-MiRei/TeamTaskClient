using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByProjectId
{
    public class GetTeamsByProjectIdHandler(ITeamRepository teamRepository) : IRequestHandler<GetTeamsByProjectIdCommand, List<TeamModel>>
    {
        public Task<List<TeamModel>> Handle(GetTeamsByProjectIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teams = teamRepository.GetTeamsByUserId(request.ProjectId);
                if (teams == null || teams.Result.Count == 0)
                {
                    throw new NotFoundException();
                }
                return teams;
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}
