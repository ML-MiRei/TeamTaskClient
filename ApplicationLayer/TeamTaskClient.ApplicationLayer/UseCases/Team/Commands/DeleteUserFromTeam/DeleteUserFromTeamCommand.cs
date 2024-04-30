using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.DeleteUserFromTeam
{
    public class DeleteUserFromTeamCommand : IRequest
    {
        public string Tag { get; set; }
        public int TeamId { get; set; }
    }
}
