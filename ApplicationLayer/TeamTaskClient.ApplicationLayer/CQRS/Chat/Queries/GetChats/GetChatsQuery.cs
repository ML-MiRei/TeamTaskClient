using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats
{
    public class GetChatsQuery : IRequest<List<ChatEntity>>
    {
        public int UserId {  get; set; }
    }
}
