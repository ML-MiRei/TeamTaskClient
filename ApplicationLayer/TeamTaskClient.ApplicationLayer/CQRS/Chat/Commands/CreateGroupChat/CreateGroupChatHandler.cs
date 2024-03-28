using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.Domain.Exceptions;


namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreateGroupChat
{
    public class CreateGroupChatHandler(IChatRepository chatRepository) : IRequestHandler<CreateGroupChatCommand, ChatEntity>
    {
        public Task<ChatEntity> Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ChatDTO chatDTO = new() { AdminId = request.UserId, Name = request.Name, ChatType = (int)ChatTypeEnum.GROUP };
                var chat = chatRepository.CreateChat(chatDTO).Result;
                return Task.FromResult(chat);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
