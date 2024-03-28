using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.SendMessage
{
    public class SendMessageHandler(IMessageRepository messageRepository) : IRequestHandler<SendMessageCommand, MessageEntity>
    {
        public Task<MessageEntity> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = messageRepository.CreateMessage(new DTOs.MessageDTO() { ChatID = request.ChatId, Text = request.TextMessage, UserID = request.UderId}); 
                return message;

            }
            catch (Exception)
            {
                throw new AddException();
            }
        }
    }
}
