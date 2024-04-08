using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Json;


using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Interfaces;

namespace TeamTaskClient.Infrastructure.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {

        private static HttpClient client = new HttpClient();
        private static string connectionString = "https://localhost:7130/api";


        public async Task<UserEntity> Authorize(string email, string password)
        {
            var httpReply = await client.GetAsync($"{connectionString}/Authentication/authenticate/email={email}&password={password}");
            var user = await httpReply.Content.ReadFromJsonAsync<UserEntity>();
            return user;
        }

        public async Task<UserEntity> Register(UserEntity userData)
        {
            try
            {

                var content = JsonContent.Create(userData);
                var httpReply = await client.PostAsync($"https://localhost:7130/api/Authentication/registration", content);

                if (httpReply.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var user = await httpReply.Content.ReadFromJsonAsync<UserEntity>();

                    return user;
                }
                throw new ConnectionException();

            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
