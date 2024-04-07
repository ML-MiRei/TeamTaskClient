using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.ChangeStatusProjectTask
{
    public class ChangeStatusProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public StatusProjectTaskEnum Status { get; set; }
    }
}
