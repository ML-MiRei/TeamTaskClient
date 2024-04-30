using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.AddUserInProject
{
    public class AddUserInProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public string UserTag { get; set; }
    }
}
