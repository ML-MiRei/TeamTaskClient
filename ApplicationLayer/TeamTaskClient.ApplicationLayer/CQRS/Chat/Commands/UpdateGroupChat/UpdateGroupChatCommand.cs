using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.UpdateGroupChat
{
    public class UpdateGroupChatCommand : IRequest
    {
        public int ChatId { get; set;}
        public string ChatName { get; set;}
        public int UserId { get; set;}
        public int AdminId { get; set;}
    }
}
