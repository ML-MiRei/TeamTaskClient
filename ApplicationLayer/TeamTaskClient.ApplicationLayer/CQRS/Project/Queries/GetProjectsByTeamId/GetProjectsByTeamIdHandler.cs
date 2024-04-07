using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Queries.GetProjectsByTeamId
{
    public class GetProjectsByUserIdHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectsByUserIdQuery, List<ProjectModel>>
    {
        public Task<List<ProjectModel>> Handle(GetProjectsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var projects = projectRepository.GetProjectsByTeamId(request.TeamId);
                if (projects == null || projects.Result.Count == 0)
                {
                    throw new NotFoundException();
                }
                return projects;
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}
