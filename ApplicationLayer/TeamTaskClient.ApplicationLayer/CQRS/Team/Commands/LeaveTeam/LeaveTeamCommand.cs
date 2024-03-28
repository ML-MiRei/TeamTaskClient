﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Commands.LeaveTeam
{
    internal class LeaveTeamCommand : IRequest
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}