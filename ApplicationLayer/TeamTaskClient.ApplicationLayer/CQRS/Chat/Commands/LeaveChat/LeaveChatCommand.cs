using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.LeaveChat
{
    public class LeaveChatCommand : IRequest
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
    }
}
