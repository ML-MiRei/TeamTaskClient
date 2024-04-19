using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreatePrivateChat
{
    public class CreatePrivateChatCommand : IRequest<ChatModel>
    {
        public int UserId { get; set; }
        public string SecondUserTag { get; set; }
    }



}
