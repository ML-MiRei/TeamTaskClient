using MediatR;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserModel>
    {
        public int UserId { get; set; }
    }
}
