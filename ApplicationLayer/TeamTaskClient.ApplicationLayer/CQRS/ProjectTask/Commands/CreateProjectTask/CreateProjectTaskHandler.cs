using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<CreateProjectTaskCommand, ProjectTaskModel>
    {
        public Task<ProjectTaskModel> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projectTask = projectTaskRepository.CreateProjectTask(new ProjectTaskEntity()
                {
                    Detail = request.Detail,
                    Title = request.Title,
                    SprintId = request.SprintId
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
