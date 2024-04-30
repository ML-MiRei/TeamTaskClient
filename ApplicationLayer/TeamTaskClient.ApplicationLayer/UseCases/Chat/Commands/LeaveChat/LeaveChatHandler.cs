using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.LeaveChat
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
