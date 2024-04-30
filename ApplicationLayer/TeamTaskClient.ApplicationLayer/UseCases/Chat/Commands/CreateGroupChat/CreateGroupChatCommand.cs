using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChat
{

    public class CreateGroupChatCommand : IRequest
    {
        public required string Name { get; set; }
        public required int UserId { get; set; }
    }




}
