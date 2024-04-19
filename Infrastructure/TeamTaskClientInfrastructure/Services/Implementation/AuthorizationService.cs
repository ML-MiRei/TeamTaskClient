using System.Net.Http.Json;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
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
                var httpReply =  client.PostAsync($"https://localhost:7130/api/Authentication/registration", content).Result;

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
