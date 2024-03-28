using AutoMapper;
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

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreatePrivateChat
{
    public class CreatePrivateChatHandler(IChatRepository chatRepository) : IRequestHandler<CreatePrivateChatCommand, ChatEntity>
    {

        Task<ChatEntity> IRequestHandler<CreatePrivateChatCommand, ChatEntity>.Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ChatDTO chatDTO = new() { AdminId = request.UserId, ChatType = (int)ChatTypeEnum.PRIVATE };
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
