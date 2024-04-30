using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.DeleteUserFromProject
{
    public class DeleteUserFromProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public string UserTag { get; set; }
    }
}
