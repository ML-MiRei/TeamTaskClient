using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Message.Commands.DeleteMessage
{
    public class DeleteMessageHandler(IMessageRepository messageRepository) : IRequestHandler<DeleteMessageCommand>
    {
        public Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                messageRepository.DeleteMessage(chatId: request.ChatId, messageId: request.MessageId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new DeleteException();
            }
        }
    }
}
