using MediatR;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.ChangeStatusProjectTask
{
    public class ChangeStatusProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public int ProjectId { get; set; }
        public int SprintId { get; set; }
        public int Status { get; set; }
    }
}
