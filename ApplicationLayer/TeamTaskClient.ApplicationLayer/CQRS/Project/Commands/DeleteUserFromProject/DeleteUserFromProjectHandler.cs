﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteUserFromProject
{
    internal class DeleteUserFromProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteUserFromProjectCommand>
    {
        public Task Handle(DeleteUserFromProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                projectRepository.DeleteUserFromProject(request.ProjectId, request.UserTag);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}