using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask
{
    internal class CreateProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<CreateProjectTaskCommand, ProjectTaskEntity>
    {
        public Task<ProjectTaskEntity> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projectTask = projectTaskRepository.CreateProjectTask(new DTOs.ProjectTaskDTO()
                {
                    DateEnd = request.DateEnd,
                    DateStart = request.DateStart,
                    StatusProjectTask = Domain.Enums.StatusProjectTaskEnum.STORIES,
                    Detail = request.Detail,
                    IdProject = request.ProjectId,
                    Title = request.Title
                });

                return projectTask;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
