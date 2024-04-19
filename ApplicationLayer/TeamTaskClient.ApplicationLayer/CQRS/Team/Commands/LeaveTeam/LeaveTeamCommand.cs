using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.LeaveTeam
{
    public class LeaveTeamCommand : IRequest
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}
