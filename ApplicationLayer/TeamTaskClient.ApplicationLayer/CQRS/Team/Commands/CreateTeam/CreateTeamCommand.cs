using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<TeamModel>
    {
        public int UserId { get; set; }
        public string Name { get; set; }

    }
}
