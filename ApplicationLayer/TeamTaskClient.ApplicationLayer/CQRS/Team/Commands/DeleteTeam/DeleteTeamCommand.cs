using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.DeleteTeam
{
    internal class DeleteTeamCommand : IRequest
    {
        public int TeamId { get; set; }
    }
}
