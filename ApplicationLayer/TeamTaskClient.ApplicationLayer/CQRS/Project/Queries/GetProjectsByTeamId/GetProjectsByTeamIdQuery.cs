using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Queries.GetProjectsByTeamId
{
    internal class GetProjectsByUserIdQuery : IRequest<List<ProjectEntity>>
    {
        public int TeamId { get; set; }
    }
}
