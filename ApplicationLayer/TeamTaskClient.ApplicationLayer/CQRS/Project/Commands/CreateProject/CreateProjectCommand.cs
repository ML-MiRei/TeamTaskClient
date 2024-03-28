using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject
{
    internal class CreateProjectCommand : IRequest<ProjectEntity>
    {
        public string UserTag { get; set; }
        public string ProjectName { get; set; }
    }
}
