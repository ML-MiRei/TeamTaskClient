using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;


using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Interfaces;

namespace TeamTaskClient.Infrastructure.Services.Implementation
{
    public class AuthorizationService(IHttpClient client) : IAuthorizationService
    {


        public async Task<UserEntity> Authorize(string email, string password)
        {
            var httpReply = await client.CurrentHttpClient.GetAsync($"{client.ConnectionString}/Login/authorize/email={email}&password={password}");
            var user = await httpReply.Content.ReadFromJsonAsync<UserEntity>();
            return user;
        }

        public Task<UserEntity> Register(UserDTO user)
        {
            return null;
        }

    }
}
