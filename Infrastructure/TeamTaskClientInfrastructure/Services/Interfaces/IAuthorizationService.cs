
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<UserEntity> Authorize(string email, string password);
        Task<UserEntity> Register(UserEntity user);
    }
}
