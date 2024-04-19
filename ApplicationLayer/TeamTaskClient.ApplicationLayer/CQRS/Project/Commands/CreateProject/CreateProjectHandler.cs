using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject
{
    public class CreateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, ProjectModel>
    {
        public Task<ProjectModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = projectRepository.CreateProject(request.ProjectName);
                return project;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
