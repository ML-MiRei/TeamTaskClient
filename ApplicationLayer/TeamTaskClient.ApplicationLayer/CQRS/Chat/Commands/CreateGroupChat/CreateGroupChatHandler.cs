using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;


namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreateGroupChat
{
    public class CreateGroupChatHandler(IChatRepository chatRepository) : IRequestHandler<CreateGroupChatCommand, ChatModel>
    {
        public Task<ChatModel> Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chat = chatRepository.CreateGroupChat(request.UserId, request.Name).Result;
                return Task.FromResult(chat);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
