using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreateGroupChat
{

    public class CreateGroupChatCommand : IRequest<ChatEntity>
    {
        public required string Name { get; set; }
        public required int UserId { get; set; }
    }




}
