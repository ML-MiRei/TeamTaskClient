using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }

    }
}
