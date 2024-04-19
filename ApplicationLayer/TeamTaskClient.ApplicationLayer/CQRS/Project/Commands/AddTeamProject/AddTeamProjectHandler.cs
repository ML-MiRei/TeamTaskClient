using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.AddTeamProject
{
    public class AddTeamProjectHandler(IProjectRepository projectRepository) : IRequestHandler<AddTeamProjectCommand>
    {
        public Task Handle(AddTeamProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.AddTeamInProject(request.ProjectId, request.TeamTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
