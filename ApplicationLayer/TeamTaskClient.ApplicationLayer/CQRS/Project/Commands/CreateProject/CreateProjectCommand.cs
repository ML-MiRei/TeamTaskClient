using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<ProjectModel>
    {
        public string ProjectName { get; set; }
    }
}
