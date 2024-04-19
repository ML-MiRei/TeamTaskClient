using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByTag(string tag);
        Task<UserEntity> GetUserById(int id);
        Task DeleteUser(int id);
        Task UpdateUser(UserEntity user);


    }
}
