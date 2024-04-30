using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;


namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChat
{
    public class CreateGroupChatHandler(IChatRepository chatRepository) : IRequestHandler<CreateGroupChatCommand>
    {
        public async Task Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await chatRepository.CreateGroupChat(request.UserId, request.Name);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
