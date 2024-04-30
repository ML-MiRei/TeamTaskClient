using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreatePrivateChat
{
    public class CreatePrivateChatHandler(IChatRepository chatRepository) : IRequestHandler<CreatePrivateChatCommand>
    {
        public Task Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // var chat = chatRepository.CreatePrivateChat(request.UserId, request.SecondUserTag).Result;
                chatRepository.CreatePrivateChat(request.UserId, request.SecondUserTag);
                return Task.CompletedTask;
                // return Task.FromResult(chat);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }


}
