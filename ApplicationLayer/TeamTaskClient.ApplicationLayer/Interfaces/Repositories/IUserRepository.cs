using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByTag(string tag);
        Task<UserModel> GetUserById(int id);
        Task DeleteUser(int id);
        Task UpdateUser(UserEntity user);


    }
}
