using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByProjectId
{
    public class GetTeamsByProjectIdCommand : IRequest<List<TeamModel>>
    {
        public int ProjectId { get; set; }
    }
}
