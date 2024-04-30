using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest
    {
        public int UserId { get; set; }
        public string ProjectName { get; set; }
    }
}
