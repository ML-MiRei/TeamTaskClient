using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Commands.DeleteUser
{
    public class DeleteUserHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
    {
        public Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                userRepository.DeleteUser(request.UserId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
