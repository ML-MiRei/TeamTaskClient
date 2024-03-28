using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam
{
    internal class CreateTeamHandler(ITeamRepository teamRepository) : IRequestHandler<CreateTeamCommand, TeamEntity>
    {
        public Task<TeamEntity> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var team = teamRepository.CreateTeam(new DTOs.TeamDTO() { Name = request.Name, LeadTag = request.UserTag});
                return team;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
