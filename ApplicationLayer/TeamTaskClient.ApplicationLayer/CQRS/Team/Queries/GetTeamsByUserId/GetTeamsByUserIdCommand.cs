using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId
{
    public class GetTeamsByUserIdCommand : IRequest<List<TeamModel>>
    {
        public int UserId { get; set; }
    }
}
