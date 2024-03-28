using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Commands.CreatePrivateChat
{
    internal class CreatePrivateChatCommand : IRequest<ChatEntity>
    {
        public int UserId { get; set; }
    }



}
