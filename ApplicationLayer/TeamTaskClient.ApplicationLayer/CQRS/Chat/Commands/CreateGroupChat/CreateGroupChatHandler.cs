using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
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
