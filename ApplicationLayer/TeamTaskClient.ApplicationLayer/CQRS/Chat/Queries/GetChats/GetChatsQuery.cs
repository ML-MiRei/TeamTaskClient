﻿using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats
{
    public class GetChatsQuery : IRequest<List<ChatModel>>
    {
        public int UserId {  get; set; }
    }
}
