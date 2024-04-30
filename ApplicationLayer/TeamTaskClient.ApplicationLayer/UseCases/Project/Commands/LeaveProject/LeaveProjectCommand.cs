using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.LeaveProject
{
    public class LeaveProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
