using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam
{
    public class CreateTeamHandler(ITeamRepository teamRepository) : IRequestHandler<CreateTeamCommand>
    {
        public Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.CreateTeam(new TeamEntity() { Name = request.Name, TeamLeadId = request.UserId });
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
