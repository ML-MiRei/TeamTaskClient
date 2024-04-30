using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.DeleteUserFromGroupChat
{
    public class DeleteUserFromGroupChatHandler(IChatRepository chatRepository) : IRequestHandler<DeleteUserFromGroupChatCommand>
    {
        public Task Handle(DeleteUserFromGroupChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                chatRepository.DeleteUserFromChatByTag(tag: request.UserTag, idChat: request.ChatId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new AddException();
            }
        }
    }
}
