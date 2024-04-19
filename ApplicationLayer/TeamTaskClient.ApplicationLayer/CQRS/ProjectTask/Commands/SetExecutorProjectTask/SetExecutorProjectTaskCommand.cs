using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.SetExecutorProjectTask
{
    public class SetExecutorProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public UserModel User { get; set; }

    }
}
