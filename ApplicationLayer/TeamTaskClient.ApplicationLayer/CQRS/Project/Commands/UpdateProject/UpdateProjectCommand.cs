using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.UpdateProject
{
    internal class UpdateProjectCommand : IRequest
    {
        public string? ProjectName { get; set;}
        public int ProjectId { get; set;}
        public string? LeadTag {  get; set;}
    }
}
