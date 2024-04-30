using MediatR;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Queries.GetChats
{
    public class GetChatsQuery : IRequest<List<ChatModel>>
    {
        public int UserId { get; set; }
    }
}
