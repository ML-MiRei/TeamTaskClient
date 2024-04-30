using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.LeaveChat
{
    public class LeaveChatCommand : IRequest
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
    }
}
