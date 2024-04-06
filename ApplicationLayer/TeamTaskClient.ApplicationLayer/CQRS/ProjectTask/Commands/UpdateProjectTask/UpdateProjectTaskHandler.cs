using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.UpdateProjectTask;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<UpdateProjectTaskCommand>
    {
        public Task Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projectTask = projectTaskRepository.UpdateProjectTask(new DTOs.ProjectTaskDTO()
                {
                    DateEnd = request.DateEnd,
                    DateStart = request.DateStart,
                    StatusProjectTask = Domain.Enums.StatusProjectTaskEnum.STORIES,
                    Detail = request.Detail,
                    IdProject = request.ProjectId,
                    Title = request.Title,
                    ID = request.ProjectTaskId,
                    ExecutorProjectTask = request.executorProjectTask?.ID
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
