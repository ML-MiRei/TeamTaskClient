﻿using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Commands.LeaveTeam
{
    public class LeaveTeamHandler(ITeamRepository teamRepository) : IRequestHandler<LeaveTeamCommand>
    {
        public Task Handle(LeaveTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                teamRepository.LeaveTeam(request.UserId, request.TeamId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
