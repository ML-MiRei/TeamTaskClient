using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.DeleteMessage
{
    internal class DeleteMessageCommand : IRequest
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
    }
}
