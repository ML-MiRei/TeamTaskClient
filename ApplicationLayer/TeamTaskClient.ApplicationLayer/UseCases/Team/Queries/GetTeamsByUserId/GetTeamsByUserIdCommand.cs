﻿using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Team.Queries.GetTeamsByUserId
{
    public class GetTeamsByUserIdCommand : IRequest<List<TeamModel>>
    {
        public int UserId { get; set; }
    }
}
