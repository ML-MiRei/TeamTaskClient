using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Queries.GetProjectsByUserId
{
    public class GetProjectsByUserIdHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectsByUserIdQuery, List<ProjectModel>>
    {
        public Task<List<ProjectModel>> Handle(GetProjectsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var projects = projectRepository.GetProjectsByUserId(request.UserId);
                if (projects == null || projects.Result.Count == 0)
                {
                    return Task.FromResult(new List<ProjectModel>());
                }
                return projects;
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}
