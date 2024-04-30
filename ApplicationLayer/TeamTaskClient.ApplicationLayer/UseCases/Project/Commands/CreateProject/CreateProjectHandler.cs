using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.CreateProject
{
    public class CreateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand>
    {
        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await projectRepository.CreateProject(request.UserId, request.ProjectName);

            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
