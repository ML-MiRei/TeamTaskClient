using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject
{
    public class CreateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, ProjectEntity>
    {
        public Task<ProjectEntity> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = projectRepository.CreateProject(new DTOs.ProjectDTO() { LeadTag = request.UserTag, Name = request.ProjectName });
                return project;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
