using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.LeaveProject
{
    public class LeaveProjectHandler(IProjectRepository projectRepository) : IRequestHandler<LeaveProjectCommand>
    {
        public Task Handle(LeaveProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.LeaveFromProject(request.ProjectId, request.UserId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
