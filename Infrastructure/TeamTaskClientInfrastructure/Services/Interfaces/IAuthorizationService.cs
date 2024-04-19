using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<UserEntity> Authorize(string email, string password);
        Task<UserEntity> Register(UserEntity user);
    }
}
