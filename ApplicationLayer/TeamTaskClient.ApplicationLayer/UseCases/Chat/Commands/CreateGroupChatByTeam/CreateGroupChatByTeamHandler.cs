using MediatR;
using TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChat;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;


namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChatByTeam
{
    public class CreateGroupChatByTeamHandler(IChatRepository chatRepository) : IRequestHandler<CreateGroupChatByTeamCommand>
    {
        public async Task Handle(CreateGroupChatByTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await chatRepository.CreateGroupChatByTeam(request.UserId, request.TeamId);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
