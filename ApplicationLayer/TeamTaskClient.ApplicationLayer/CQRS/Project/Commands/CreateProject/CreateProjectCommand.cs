using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<ProjectModel>
    {
        public string ProjectName { get; set; }
    }
}
