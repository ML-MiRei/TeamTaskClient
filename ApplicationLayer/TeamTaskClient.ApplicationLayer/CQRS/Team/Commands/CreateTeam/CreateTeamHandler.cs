using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam
{
    public class CreateTeamHandler(ITeamRepository teamRepository) : IRequestHandler<CreateTeamCommand, TeamModel>
    {
        public Task<TeamModel> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var team = teamRepository.CreateTeam(new TeamEntity() { Name = request.Name, TeamLeadTag = request.UserTag});
                return team;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
