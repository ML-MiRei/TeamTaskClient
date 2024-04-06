using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest
    {
        public string? ProjectName { get; set;}
        public int ProjectId { get; set;}
        public string? LeadTag {  get; set;}
    }
}
