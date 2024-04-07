using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.ChangeStatusProjectTask
{
    public class ChangeStatusProjectTaskHandler(IProjectTaskRepository projectTaskRepository) : IRequestHandler<ChangeStatusProjectTaskCommand>
    {
        public Task Handle(ChangeStatusProjectTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectTaskRepository.ChangeStatusProjectTask(request.ProjectTaskId, request.Status);
                return Task.CompletedTask;
            }
            catch(Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
