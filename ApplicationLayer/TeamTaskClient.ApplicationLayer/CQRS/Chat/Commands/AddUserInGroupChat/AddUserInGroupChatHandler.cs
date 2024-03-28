using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.AddUserInGroupChat
{
    public class AddUserInGroupChatHandler(IChatRepository chatRepository) : IRequestHandler<AddUserInGroupChatCommand>
    {
        public Task Handle(AddUserInGroupChatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                chatRepository.AddUserGroupChatByTag(tag: request.UserTag, idChat: request.ChatId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new AddException();
            }
        }
    }
}
