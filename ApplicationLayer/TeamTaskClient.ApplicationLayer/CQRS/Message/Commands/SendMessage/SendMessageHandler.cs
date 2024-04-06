using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.SendMessage
{
    public class SendMessageHandler(IMessageRepository messageRepository) : IRequestHandler<SendMessageCommand>
    {
        public Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                messageRepository.CreateMessage(new DTOs.MessageDTO() { ChatID = request.ChatId, Text = request.TextMessage, UserID = request.UderId});
                return Task.CompletedTask;

            }
            catch (Exception)
            {
                throw new AddException();
            }
        }
    }
}
