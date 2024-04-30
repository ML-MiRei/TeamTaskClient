using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChat
{

    public class CreateGroupChatByTeamCommand : IRequest
    {
        public required int TeamId { get; set; }
        public required int UserId { get; set; }
    }




}
