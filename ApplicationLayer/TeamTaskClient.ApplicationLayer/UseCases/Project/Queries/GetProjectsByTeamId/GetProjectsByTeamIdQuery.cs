using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Queries.GetProjectsByTeamId
{
    public class GetProjectsByUserIdQuery : IRequest<List<ProjectModel>>
    {
        public int TeamId { get; set; }
    }
}
