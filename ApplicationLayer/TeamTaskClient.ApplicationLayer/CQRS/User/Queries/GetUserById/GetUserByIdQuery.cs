﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById
{
    internal class GetUserByTeamQuery : IRequest<UserEntity>
    {
        public int UserId { get; set; }
    }
}