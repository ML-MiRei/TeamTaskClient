using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.UpdateGroupChat
{
    public class UpdateGroupChatHandler(IChatRepository chatRepository) : IRequestHandler<UpdateGroupChatCommand>
    {
        public Task Handle(UpdateGroupChatCommand request, CancellationToken cancellationToken)
        {
            try
            {

                chatRepository.UpdateChat(new ChatEntity() { AdminTag = request.AdminTag, ChatName = request.ChatName });
                return Task.CompletedTask;

            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
