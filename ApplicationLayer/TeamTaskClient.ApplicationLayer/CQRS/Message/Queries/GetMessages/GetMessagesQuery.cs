using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Queries.GetMessages
{
    internal class GetMessagesQuery : IRequest<List<MessageEntity>>
    {
        public int ChatId { get; set; }
    }
}
