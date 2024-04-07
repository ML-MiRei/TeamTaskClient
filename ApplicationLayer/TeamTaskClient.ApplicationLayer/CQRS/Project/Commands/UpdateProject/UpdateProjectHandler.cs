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
    public class UpdateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand>
    {
        public Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.UpdateProject(new Domain.Entities.ProjectEntity() { ProjectLeadTag = request.LeadTag, ProjectName = request.ProjectName, ID = request.ProjectId});
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
