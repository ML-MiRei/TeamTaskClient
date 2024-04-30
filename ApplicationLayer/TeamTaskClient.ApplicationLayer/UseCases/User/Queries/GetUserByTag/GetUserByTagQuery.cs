using MediatR;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.UseCases.User.Queries.GetUserByTag
{
    public class GetUserByTagQuery : IRequest<UserModel>
    {
        public string UserTag { get; set; }
    }
}
