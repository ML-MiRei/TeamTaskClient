using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }

    }
}
