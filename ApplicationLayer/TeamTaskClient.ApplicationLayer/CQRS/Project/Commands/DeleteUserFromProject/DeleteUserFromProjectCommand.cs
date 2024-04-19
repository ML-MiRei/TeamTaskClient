using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteUserFromProject
{
    public class DeleteUserFromProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public string UserTag { get; set; }
    }
}
