using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.DeleteProjectTask
{
    internal class DeleteProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<DeleteProjectTaskCommand>
    {
        public Task Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectTaskRepository.DeleteProjectTask(request.ProjectId, request.ProjectTaskId);
                return Task.CompletedTask;
            }
            catch(Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
