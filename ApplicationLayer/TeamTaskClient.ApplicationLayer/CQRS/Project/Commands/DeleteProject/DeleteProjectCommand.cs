using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
    }
}
