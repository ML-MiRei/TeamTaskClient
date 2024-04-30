using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<DeleteProjectTaskCommand>
    {
        public Task Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectTaskRepository.DeleteProjectTask(request.ProjectId, request.ProjectTaskId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
