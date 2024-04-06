using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                messageRepository.UpdateMessage(new DTOs.MessageDTO() { ChatID = request.ChatId, Text = request.TextMessage, ID = request.MessageId });
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
