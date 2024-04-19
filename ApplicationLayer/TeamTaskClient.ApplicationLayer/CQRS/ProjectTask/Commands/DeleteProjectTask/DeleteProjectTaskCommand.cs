
using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int ProjectTaskId { get; set; }
    }
}
