using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteUserFromTeam
{
    internal class DeleteUserFromTeamCommand : IRequest
    {
        public string Tag { get; set; }
        public int TeamId { get; set; }
    }
}
