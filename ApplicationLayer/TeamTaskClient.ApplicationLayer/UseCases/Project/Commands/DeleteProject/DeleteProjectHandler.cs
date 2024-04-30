using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.DeleteProject
{
    public class DeleteProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand>
    {
        public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
               await projectRepository.DeleteProject(request.ProjectId);
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
