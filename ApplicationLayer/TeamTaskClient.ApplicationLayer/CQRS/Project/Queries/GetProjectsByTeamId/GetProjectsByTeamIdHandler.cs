﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Queries.GetProjectsByTeamId
{
    internal class GetProjectsByUserIdHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectsByUserIdQuery, List<ProjectEntity>>
    {
        public Task<List<ProjectEntity>> Handle(GetProjectsByUserIdQuery request, CancellationToken cancellationToken)
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