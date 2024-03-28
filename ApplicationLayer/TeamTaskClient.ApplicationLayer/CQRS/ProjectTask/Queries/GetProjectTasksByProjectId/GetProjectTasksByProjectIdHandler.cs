using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Queries.GetProjectTasksByProjectId
{

    internal class GetProjectTasksByProjectIdHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<GetProjectTasksByProjectIdQuery, List<ProjectTaskEntity>>
    {
        public Task<List<ProjectTaskEntity>> Handle(GetProjectTasksByProjectIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var projectTasks = projectTaskRepository.GetProjectTasksByProjectId(request.ProjectId);
                if (projectTasks == null || projectTasks.Result.Count == 0)
                {
                    throw new NotFoundException();
                }
                return projectTasks;
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }

        }
    }
}
