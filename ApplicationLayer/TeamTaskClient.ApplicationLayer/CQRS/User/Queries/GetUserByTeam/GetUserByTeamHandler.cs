using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTeam
{
    internal class GetUserByTeamHandler(IUserRepository userRepository) : IRequestHandler<GetUserByTeamQuery, List<UserEntity>>
    {
        public Task<List<UserEntity>> Handle(GetUserByTeamQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = userRepository.GetUsersByTeam(request.TeamId);
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
