using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.AddUserInTeam
{
    public class AddUserInTeamCommand : IRequest
    {
        public string TeamTag { get; set; }
        public string UserTag { get; set; }
    }
}
