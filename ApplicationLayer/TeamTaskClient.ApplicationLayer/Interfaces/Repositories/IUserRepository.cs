using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
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
