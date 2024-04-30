using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Queries.GetTeamsByProjectId
{
    public class GetTeamsByProjectIdCommand : IRequest<List<TeamModel>>
    {
        public int ProjectId { get; set; }
    }
}
