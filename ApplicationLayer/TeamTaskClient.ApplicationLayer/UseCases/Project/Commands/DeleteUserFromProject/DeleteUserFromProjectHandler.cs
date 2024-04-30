using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.DeleteUserFromProject
{
    public class DeleteUserFromProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteUserFromProjectCommand>
    {
        public Task Handle(DeleteUserFromProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.DeleteUserFromProject(request.ProjectId, request.UserTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
