using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag
{
    public class GetUserByTagHandler(IUserRepository userRepository) : IRequestHandler<GetUserByTagQuery, UserEntity>
    {
        public Task<UserEntity> Handle(GetUserByTagQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = userRepository.GetUserByTag(request.UserTag);
                if (user == null)
                {
                    throw new NotFoundException();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
