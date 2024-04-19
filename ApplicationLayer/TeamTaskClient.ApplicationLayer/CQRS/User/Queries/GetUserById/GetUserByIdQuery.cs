using MediatR;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserEntity>
    {
        public int UserId { get; set; }
    }
}
