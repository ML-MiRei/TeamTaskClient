using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.AddTeamProject
{
    public class AddTeamProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public string TeamTag { get; set; }
    }
}
