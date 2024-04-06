using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByChat
{
    public class GetUserByChatQuery : IRequest<List<UserEntity>>
    {
        public int ChatId { get; set; }
    }
}
