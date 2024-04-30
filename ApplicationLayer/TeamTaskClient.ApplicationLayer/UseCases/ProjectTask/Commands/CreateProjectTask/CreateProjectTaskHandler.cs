using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<CreateProjectTaskCommand>
    {
        public async Task Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
               await projectTaskRepository.CreateProjectTask(new ProjectTaskEntity()
                {
                    Detail = request.Detail,
                    Title = request.Title,
                    SprintId = request.SprintId,
                    ProjectId = request.ProjectId,
                    Status = request.Status.Value
                });
                

            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
