using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Queries.GetProjectsByUserId
{
    public class GetProjectsByUserIdQuery : IRequest<List<ProjectModel>>
    {
        public int UserId { get; set; }
    }
}
