using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;


namespace TeamTaskClient.ApplicationLayer.UseCases.Chat.Commands.CreateGroupChatByProject
{
    public class CreateGroupChatByProjectHandler(IChatRepository chatRepository) : IRequestHandler<CreateGroupChatByProjectCommand>
    {
        public async Task Handle(CreateGroupChatByProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await chatRepository.CreateGroupChatByProject(request.UserId, request.ProjectId);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
