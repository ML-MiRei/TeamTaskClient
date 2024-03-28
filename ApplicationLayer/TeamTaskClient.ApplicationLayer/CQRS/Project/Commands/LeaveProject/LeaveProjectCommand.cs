﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.LeaveProject
{
    internal class LeaveProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
