using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.UpdateTeam
{
    public class UpdateTeamCommand : IRequest
    {
        public string? Name { get; set; }
        public int TeamId { get; set; }
        public string? LeaderTag { get; set; }
    }
}
