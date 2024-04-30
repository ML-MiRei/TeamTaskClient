using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }

    }
}
