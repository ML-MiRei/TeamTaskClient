﻿using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public int UserId { get; set; }

    }
}
