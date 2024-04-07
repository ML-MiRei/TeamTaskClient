using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.AddUserInTeam
{
    public class AddUserInTeamHandler(ITeamRepository teamRepository) : IRequestHandler<AddUserInTeamCommand>
    {
        public Task Handle(AddUserInTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.AddUserInTeamByTag(request.TeamId, request.UserTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new AddException();
            }
        }
    }
}
