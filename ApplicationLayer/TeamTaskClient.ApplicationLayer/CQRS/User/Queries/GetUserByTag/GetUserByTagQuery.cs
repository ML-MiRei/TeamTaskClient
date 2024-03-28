﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag
{
    internal class GetUserByIdQuery : IRequest<UserEntity>
    {
        public string UserTag { get; set; }
    }
}