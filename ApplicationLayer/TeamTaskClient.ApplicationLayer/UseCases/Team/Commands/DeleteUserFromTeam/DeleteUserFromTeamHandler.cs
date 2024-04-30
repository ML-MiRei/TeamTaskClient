
using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.DeleteUserFromTeam
{
    public class DeleteUserFromTeamHandler(ITeamRepository teamRepository) : IRequestHandler<DeleteUserFromTeamCommand>
    {
        public Task Handle(DeleteUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.DeleteUserFromTeam(request.Tag, request.TeamId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
