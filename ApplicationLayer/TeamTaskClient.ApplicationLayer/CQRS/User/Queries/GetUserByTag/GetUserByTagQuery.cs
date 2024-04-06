using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag
{
    public class GetUserByTagQuery : IRequest<UserEntity>
    {
        public string UserTag { get; set; }
    }
}
