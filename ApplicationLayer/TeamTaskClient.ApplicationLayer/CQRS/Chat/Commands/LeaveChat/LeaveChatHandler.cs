using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.LeaveChat
{
    public class LeaveChatHandler(IChatRepository chatRepository) : IRequestHandler<LeaveChatCommand>
    {
        public Task Handle(LeaveChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                chatRepository.LeaveChat(userId: request.UserId, chatId: request.ChatId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
