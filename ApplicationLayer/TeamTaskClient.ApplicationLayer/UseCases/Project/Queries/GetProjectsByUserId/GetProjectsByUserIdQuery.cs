using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Queries.GetProjectsByUserId
{
    public class GetProjectsByUserIdQuery : IRequest<List<ProjectModel>>
    {
        public int UserId { get; set; }
    }
}
