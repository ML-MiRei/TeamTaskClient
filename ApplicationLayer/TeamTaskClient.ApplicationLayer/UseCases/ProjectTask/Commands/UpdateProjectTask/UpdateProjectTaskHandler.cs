using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<UpdateProjectTaskCommand>
    {
        public Task Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projectTask = projectTaskRepository.UpdateProjectTask(new ProjectTaskEntity()
                {
                    Detail = request.Detail,
                    Title = request.Title,
                    ID = request.ProjectTaskId,
                    ProjectId = request.ProjectId,
                });

                return projectTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
