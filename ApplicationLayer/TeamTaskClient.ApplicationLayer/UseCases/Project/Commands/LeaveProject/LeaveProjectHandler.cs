using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.LeaveProject
{
    public class LeaveProjectHandler(IProjectRepository projectRepository) : IRequestHandler<LeaveProjectCommand>
    {
        public Task Handle(LeaveProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.LeaveFromProject(request.UserId, request.ProjectId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
