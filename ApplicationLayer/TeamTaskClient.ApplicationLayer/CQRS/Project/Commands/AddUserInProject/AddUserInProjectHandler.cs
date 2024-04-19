using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.AddUserInProject
{
    public class AddUserInProjectHandler(IProjectRepository projectRepository) : IRequestHandler<AddUserInProjectCommand>
    {
        public Task Handle(AddUserInProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.AddUserInProject(request.ProjectId, request.UserTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
