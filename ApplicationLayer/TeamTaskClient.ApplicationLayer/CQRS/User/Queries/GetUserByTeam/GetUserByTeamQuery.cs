using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTeam
{
    internal class GetUserByTeamQuery : IRequest<List<UserEntity>>
    {
        public int TeamId { get; set; }
    }
}
