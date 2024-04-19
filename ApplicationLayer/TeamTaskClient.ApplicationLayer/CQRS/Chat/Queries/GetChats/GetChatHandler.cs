using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats
{
    public class GetChatHandler(IChatRepository chatRepository) : IRequestHandler<GetChatsQuery, List<ChatModel>>
    {
        public Task<List<ChatModel>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var chats = chatRepository.GetChatByIdUser(request.UserId).Result;
                return Task.FromResult(chats);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<ChatModel>());
            }
        }
    }
}
