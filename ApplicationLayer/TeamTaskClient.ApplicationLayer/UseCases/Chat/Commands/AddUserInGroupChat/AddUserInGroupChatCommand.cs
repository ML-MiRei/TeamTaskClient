using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.AddUserInGroupChat
{
    public class AddUserInGroupChatCommand : IRequest
    {
        public string UserTag { get; set; }
        public int ChatId { get; set; }
    }
}
