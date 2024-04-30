using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Project.Commands.AddTeamProject
{
    public class AddTeamProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public string TeamTag { get; set; }
    }
}
