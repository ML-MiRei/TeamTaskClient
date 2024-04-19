using MediatR;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.ChangeStatusProjectTask
{
    public class ChangeStatusProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public int ProjectId { get; set; }
        public int Status { get; set; }
    }
}
