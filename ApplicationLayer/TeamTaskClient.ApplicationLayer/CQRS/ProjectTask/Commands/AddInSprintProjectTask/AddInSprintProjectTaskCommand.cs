using MediatR;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.AddInSprintProjectTask
{
    public class AddInSprintProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public int SprintId { get; set; }
    }
}
