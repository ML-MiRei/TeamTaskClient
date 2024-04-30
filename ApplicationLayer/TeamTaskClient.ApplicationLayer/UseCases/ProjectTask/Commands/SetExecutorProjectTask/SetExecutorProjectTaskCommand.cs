using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.ProjectTask.Commands.SetExecutorProjectTask
{
    public class SetExecutorProjectTaskCommand : IRequest
    {
        public int ProjectTaskId { get; set; }
        public int ProjectId { get; set; }
        public UserModel User { get; set; }

    }
}
