using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreateGroupChat
{

    public class CreateGroupChatCommand : IRequest<ChatModel>
    {
        public required string Name { get; set; }
        public required int UserId { get; set; }
    }




}
