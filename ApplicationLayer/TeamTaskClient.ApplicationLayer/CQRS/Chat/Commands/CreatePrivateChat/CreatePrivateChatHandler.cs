using AutoMapper;
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

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreatePrivateChat
{
    public class CreatePrivateChatHandler(IChatRepository chatRepository) : IRequestHandler<CreatePrivateChatCommand, ChatModel>
    {

        Task<ChatModel> IRequestHandler<CreatePrivateChatCommand, ChatModel>.Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chat = chatRepository.CreatePrivateChat(request.UserId, request.SecondUserTag).Result;
                return Task.FromResult(chat);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
