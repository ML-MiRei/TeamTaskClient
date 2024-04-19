using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteUserFromTeam
{
    public class DeleteUserFromTeamCommand : IRequest
    {
        public string Tag { get; set; }
        public int TeamId { get; set; }
    }
}
