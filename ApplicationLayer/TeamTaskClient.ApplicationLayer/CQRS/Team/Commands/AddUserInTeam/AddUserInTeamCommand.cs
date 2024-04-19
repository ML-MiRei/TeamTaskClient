using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.AddUserInTeam
{
    public class AddUserInTeamCommand : IRequest
    {
        public int TeamId { get; set; }
        public string UserTag { get; set; }
    }
}
