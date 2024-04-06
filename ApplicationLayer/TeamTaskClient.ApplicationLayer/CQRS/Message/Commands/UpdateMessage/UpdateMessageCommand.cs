using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string TextMessage { get; set; }
    }
}
