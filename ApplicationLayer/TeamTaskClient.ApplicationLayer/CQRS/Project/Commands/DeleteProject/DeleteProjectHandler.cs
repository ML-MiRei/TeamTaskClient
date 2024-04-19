using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteProject
{
    public class DeleteProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand>
    {
        public Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.DeleteProject(request.ProjectId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
