using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.UpdateProject
{
    internal class UpdateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand>
    {
        public Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.UpdateProject(new DTOs.ProjectDTO() { LeadTag = request.LeadTag, Name = request.ProjectName, ID = request.ProjectId});
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
