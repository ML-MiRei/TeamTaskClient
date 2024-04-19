using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats
{
    public class GetChatsQuery : IRequest<List<ChatModel>>
    {
        public int UserId { get; set; }
    }
}
