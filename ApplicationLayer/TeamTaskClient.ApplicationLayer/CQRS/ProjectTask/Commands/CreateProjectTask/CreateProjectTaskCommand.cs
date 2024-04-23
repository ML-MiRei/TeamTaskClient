using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommand : IRequest
    {
        public int? SprintId { get; set; }
        public int? Status { get; set; }
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }


    }
}
