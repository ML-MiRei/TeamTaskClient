using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.LeaveProject
{
    public class LeaveProjectHandler(IProjectRepository projectRepository) : IRequestHandler<LeaveProjectCommand>
    {
        public async Task Handle(LeaveProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await projectRepository.LeaveFromProject(request.ProjectId, request.UserId);
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
