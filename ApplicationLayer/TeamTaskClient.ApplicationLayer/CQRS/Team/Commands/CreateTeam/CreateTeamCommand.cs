using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam
{
    internal class CreateTeamCommand : IRequest<TeamEntity>
    {
        public string UserTag { get; set; }
        public string Name { get; set; }

    }
}
