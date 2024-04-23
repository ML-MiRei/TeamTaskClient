using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<CreateProjectTaskCommand>
    {
        public Task Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectTaskRepository.CreateProjectTask(new ProjectTaskEntity()
                {
                    Detail = request.Detail,
                    Title = request.Title,
                    SprintId = request.SprintId,
                    ProjectId = request.ProjectId,
                    Status = request.Status.Value
                });
                
                return Task.CompletedTask;

            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
