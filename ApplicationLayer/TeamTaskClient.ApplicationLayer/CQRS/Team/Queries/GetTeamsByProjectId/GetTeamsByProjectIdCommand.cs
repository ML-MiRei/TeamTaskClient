using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByProjectId
{
    internal class GetTeamsByProjectIdCommand : IRequest<List<TeamEntity>>
    {
        public int ProjectId {  get; set; }
    }
}
