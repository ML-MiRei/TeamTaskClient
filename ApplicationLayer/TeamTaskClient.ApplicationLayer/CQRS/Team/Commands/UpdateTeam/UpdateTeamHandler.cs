using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.UpdateTeam
{
    public class UpdateTeamHandler(ITeamRepository teamRepository) : IRequestHandler<UpdateTeamCommand>
    {
        public Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.UpdateTeam(new TeamEntity() { Name = request.Name, ID = request.TeamId, TeamLeadTag = request.LeaderTag });
                return Task.CompletedTask;
            }
            catch(Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
