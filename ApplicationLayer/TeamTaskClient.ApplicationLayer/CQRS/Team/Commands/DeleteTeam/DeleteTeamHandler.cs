using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteTeam
{
    internal class DeleteTeamHandler(ITeamRepository teamRepository) : IRequestHandler<DeleteTeamCommand>
    {
        public Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.DeleteTeam(request.TeamId);
                return Task.CompletedTask;
            }
            catch(Exception)
            {
                throw new DeleteException();
            }

        }
    }
}
