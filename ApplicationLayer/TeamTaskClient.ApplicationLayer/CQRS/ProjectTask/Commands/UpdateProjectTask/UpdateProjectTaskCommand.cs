using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }

    }
}
