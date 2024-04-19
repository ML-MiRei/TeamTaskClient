using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.AddUserInGroupChat
{
    public class AddUserInGroupChatCommand : IRequest
    {
        public string UserTag { get; set; }
        public int ChatId { get; set; }
    }
}
