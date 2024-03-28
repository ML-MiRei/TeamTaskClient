using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByChat
{
    internal class GetUserByProjectQuery : IRequest<List<UserEntity>>
    {
        public int ProjectId { get; set; }
    }
}
