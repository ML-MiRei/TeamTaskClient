using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.DeleteUserFromGroupChat
{
    public class DeleteUserFromGroupChatCommand : IRequest
    {
        public string UserTag { get; set; }
        public int ChatId { get; set; }
    }
}
