using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChatByProject
{

    public class CreateGroupChatByProjectCommand : IRequest
    {
        public required int ProjectId { get; set; }
        public required int UserId { get; set; }
    }




}
