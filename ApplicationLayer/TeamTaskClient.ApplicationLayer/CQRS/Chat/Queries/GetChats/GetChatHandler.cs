using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats
{
    public class GetChatHandler(IChatRepository chatRepository) : IRequestHandler<GetChatsQuery, List<ChatEntity>>
    {
        public Task<List<ChatEntity>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var chats = chatRepository.GetChatByIdUser(request.UserId).Result;
                return Task.FromResult(chats);
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}
