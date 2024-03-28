using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByChat
{
    internal class GetUserByChatHandler(IUserRepository userRepository) : IRequestHandler<GetUserByChatQuery, List<UserEntity>>
    {
        public Task<List<UserEntity>> Handle(GetUserByChatQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = userRepository.GetUsersByChat(request.ChatId);
                if (user == null || user.Result.Count == 0)
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
