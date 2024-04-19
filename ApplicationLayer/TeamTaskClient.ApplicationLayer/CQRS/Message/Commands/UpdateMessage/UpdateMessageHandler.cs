using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.UpdateMessage
{
    public class UpdateMessageHandler(IMessageRepository messageRepository) : IRequestHandler<UpdateMessageCommand>
    {
        public Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                messageRepository.UpdateMessage(new Domain.Entities.MessageEntity() { ChatId = request.ChatId, TextMessage = request.TextMessage, ID = request.MessageId });
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
