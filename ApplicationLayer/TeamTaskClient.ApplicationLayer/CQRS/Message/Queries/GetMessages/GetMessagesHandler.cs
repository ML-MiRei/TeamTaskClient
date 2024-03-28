using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Queries.GetMessages
{
    internal class GetMessagesHandler(IMessageRepository messageRepository) : IRequestHandler<GetMessagesQuery, List<MessageEntity>>
    {
        public Task<List<MessageEntity>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var messages = messageRepository.GetMessagesByChatId(id: request.ChatId);
                if(messages == null || messages.Result.Count == 0)
                {
                    throw new NotFoundException();
                }
                return messages;
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}
