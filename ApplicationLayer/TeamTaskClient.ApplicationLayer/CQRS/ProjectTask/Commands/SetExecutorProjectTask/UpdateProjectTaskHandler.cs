using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.SetExecutorProjectTask
{
    public class SetExecutorProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<SetExecutorProjectTaskCommand>
    {
        public Task Handle(SetExecutorProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projectTask = projectTaskRepository.SetExecutorProjectTask(request.ProjectId, request.ProjectTaskId, request.User.UserTag);

                return projectTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
