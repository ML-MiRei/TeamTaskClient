using MediatR;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag
{
    public class GetUserByTagQuery : IRequest<UserEntity>
    {
        public string UserTag { get; set; }
    }
}
