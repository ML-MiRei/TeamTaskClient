using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.AddUserInGroupChat
{
    public class AddUserInGroupChatCommand : IRequest
    {
        public string UserTag {  get; set; }
        public int ChatId { get; set; }
    }
}
