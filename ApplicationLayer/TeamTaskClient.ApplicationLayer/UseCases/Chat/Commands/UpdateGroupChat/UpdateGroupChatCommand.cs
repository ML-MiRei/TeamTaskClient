using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.UpdateGroupChat
{
    public class UpdateGroupChatCommand : IRequest
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public int UserId { get; set; }
        public string AdminTag { get; set; }
    }
}
