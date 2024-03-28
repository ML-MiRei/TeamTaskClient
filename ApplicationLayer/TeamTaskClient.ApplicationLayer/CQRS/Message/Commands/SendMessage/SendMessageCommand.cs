using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage
{
    public class SendMessageCommand : IRequest<MessageEntity>
    {
        public int ChatId { get; set; }
        public int UderId { get; set; }
        public string TextMessage { get; set; }
    }
}
