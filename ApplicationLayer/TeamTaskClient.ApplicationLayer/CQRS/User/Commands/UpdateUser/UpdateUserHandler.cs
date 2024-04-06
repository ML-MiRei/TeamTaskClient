using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Commands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand>
    {
        public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                userRepository.UpdateUser(new DTOs.UserDTO()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    SecondName = request.SecondName,
                    Phone = request.Phone,
                    ID = request.UserId
                });
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
