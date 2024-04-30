using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.ChangeStatusProjectTask
{
    public class ChangeStatusProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<ChangeStatusProjectTaskCommand>
    {
        public async Task Handle(ChangeStatusProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await projectTaskRepository.ChangeStatusProjectTask(request.ProjectId, request.SprintId, request.ProjectTaskId, request.Status);
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
