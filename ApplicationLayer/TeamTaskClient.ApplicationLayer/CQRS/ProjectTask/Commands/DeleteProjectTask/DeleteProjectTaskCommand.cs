
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int ProjectTaskId { get; set; }
    }
}
