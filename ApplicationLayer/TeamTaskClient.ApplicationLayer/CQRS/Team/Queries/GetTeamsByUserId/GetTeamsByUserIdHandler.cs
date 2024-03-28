using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId
{
    internal class GetTeamsByUserIdHandler(ITeamRepository teamRepository) : IRequestHandler<GetTeamsByUserIdCommand, List<TeamEntity>>
    {
        public Task<List<TeamEntity>> Handle(GetTeamsByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teams = teamRepository.GetTeamsByUserId(request.UserId);
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
