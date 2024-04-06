using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.UpdateTeam
{
    public class UpdateTeamCommand : IRequest
    {
        public string? Name { get; set; }
        public int TeamId { get; set; }
        public string? LeaderTag { get; set; }
    }
}
