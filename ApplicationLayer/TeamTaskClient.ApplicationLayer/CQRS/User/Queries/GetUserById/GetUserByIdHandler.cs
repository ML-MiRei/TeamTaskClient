using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById
{
    public class GetUserByIdHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        public Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = userRepository.GetUserById(request.UserId);
                if (user == null)
                {
                    throw new NotFoundException();
                }
                return user;
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}
