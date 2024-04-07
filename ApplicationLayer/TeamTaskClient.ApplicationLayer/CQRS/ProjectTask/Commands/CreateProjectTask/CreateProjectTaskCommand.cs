using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommand : IRequest<ProjectTaskModel>
    {
        public int SprintId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }


    }
}
