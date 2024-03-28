using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class UserRepositoryImplementation : IUserRepository
    {
        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetUsersByChat(int idChat)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetUsersByProject(int idProject)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetUsersByTeam(int idTeam)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
