using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.UpdateTeam
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
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
