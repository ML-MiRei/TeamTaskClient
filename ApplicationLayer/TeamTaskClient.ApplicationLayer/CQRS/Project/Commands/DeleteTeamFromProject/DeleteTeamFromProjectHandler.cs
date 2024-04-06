using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteTeamFromProject
{
    public class DeleteTeamFromProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteTeamFromProjectCommand>
    {
        public Task Handle(DeleteTeamFromProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.DeleteTeamFromProject(request.ProjectId, request.TeamTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
