
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteUserFromTeam
{
    public class DeleteUserFromTeamHandler(ITeamRepository teamRepository) : IRequestHandler<DeleteUserFromTeamCommand>
    {
        public Task Handle(DeleteUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.DeleteUserFromTeam(request.Tag, request.TeamTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
