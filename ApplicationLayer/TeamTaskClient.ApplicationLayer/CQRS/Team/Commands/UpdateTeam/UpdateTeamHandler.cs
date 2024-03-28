using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.UpdateTeam
{
    internal class UpdateTeamHandler(ITeamRepository teamRepository) : IRequestHandler<UpdateTeamCommand>
    {
        public Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.UpdateTeam(new DTOs.TeamDTO() { Name = request.Name, TeamId = request.TeamId, LeadTag = request.LeaderTag });
                return Task.CompletedTask;
            }
            catch(Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
