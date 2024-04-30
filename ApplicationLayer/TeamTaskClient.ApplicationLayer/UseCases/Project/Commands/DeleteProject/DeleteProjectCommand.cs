using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
    }
}
