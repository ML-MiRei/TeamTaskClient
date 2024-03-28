
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
    internal class DeleteUserFromTeamHandler(ITeamRepository teamRepository) : IRequestHandler<DeleteUserFromTeamCommand>
    {
        public Task Handle(DeleteUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.DeletUserFromTeam(request.Tag, request.TeamId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
