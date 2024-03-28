using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Queries.GetProjectTasksByProjectId
{
    internal class GetProjectTasksByProjectIdQuery : IRequest<List<ProjectTaskEntity>>
    {
        public int ProjectId { get; set; }
    }
}
