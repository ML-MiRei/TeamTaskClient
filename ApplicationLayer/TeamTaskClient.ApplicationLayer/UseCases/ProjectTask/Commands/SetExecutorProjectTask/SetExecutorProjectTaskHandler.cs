using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.SetExecutorProjectTask
{
    public class SetExecutorProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<SetExecutorProjectTaskCommand>
    {
        public async Task Handle(SetExecutorProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await projectTaskRepository.SetExecutorProjectTask(request.ProjectId, request.ProjectTaskId, request.User.UserTag);

            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
