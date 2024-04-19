using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.AddInSprintProjectTask
{
    public class AddInSprintProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<AddInSprintProjectTaskCommand>
    {
        public Task Handle(AddInSprintProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectTaskRepository.AddInSprintProjectTask(request.ProjectTaskId, request.SprintId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
