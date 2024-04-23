using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class UserRepositoryImplementation(IHttpClient httpClient) : IUserRepository
    {

        private const string API_DOMAIN = "/User";

        public async Task DeleteUser(int id)
        {
            var httpReply = await httpClient.CurrentHttpClient.DeleteAsync($"{httpClient.ConnectionString}{API_DOMAIN}/delete");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return;
            }
            throw new ConnectionException();
        }

        public async Task<UserModel> GetUserById(int id)
        {



            var httpReply = await httpClient.CurrentHttpClient.GetAsync($"{httpClient.ConnectionString}{API_DOMAIN}/id");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var user = httpReply.Content.ReadFromJsonAsync<UserModel>();
                return user.Result;



            }
            throw new ConnectionException();
        }

        public async Task<UserModel> GetUserByTag(string userTag)
        {
            var httpReply = httpClient.CurrentHttpClient
                .GetAsync($"{httpClient.ConnectionString}{API_DOMAIN}/tag/{userTag}").Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var user = httpReply.Content.ReadFromJsonAsync<UserModel>();
                return user.Result;
            }
            throw new ConnectionException();

        }


        public async Task UpdateUser(UserEntity userData)
        {
            var httpReply = await httpClient.CurrentHttpClient.PatchAsync($"{httpClient.ConnectionString}{API_DOMAIN}/update", JsonContent.Create(userData));

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return;
            }
            throw new ConnectionException();
        }
    }
}
