using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Queries.GetProjectTasksByUserId
{
    public class GetProjectTasksByUserIdQuery : IRequest<List<ProjectTaskEntity>>
    {
        public int UserId { get; set; }
    }
}
