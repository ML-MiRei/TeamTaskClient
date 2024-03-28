using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask
{
    internal class CreateProjectTaskCommand : IRequest<ProjectTaskEntity>
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

    }
}
