using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByProjectId
{
    public class GetTeamsByProjectIdCommand : IRequest<List<TeamModel>>
    {
        public int ProjectId {  get; set; }
    }
}
