using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class UserRepositoryImplementation(IHttpClient httpClient) : IUserRepository
    {

        private const string API_DOMAIN = "/User";

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var httpReply = await httpClient.CurrentHttpClient.GetAsync($"{httpClient.ConnectionString}{API_DOMAIN}/get-user-by-id/user-id={id}");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var user = httpReply.Content.ReadFromJsonAsync<UserEntity>();
                return user.Result;

            }
            throw new ConnectionException();
        }

        public async Task<UserEntity> GetUserByTag(string tag)
        {


            var httpReply = httpClient.CurrentHttpClient.GetAsync($"{httpClient.ConnectionString}{API_DOMAIN}/get-user-by-tag/user-tag={tag}").Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var user = httpReply.Content.ReadFromJsonAsync<UserEntity>();
                return user.Result;

            }
            throw new ConnectionException();


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

        public async Task UpdateUser(UserDTO userData)
        {
            var httpReply = await httpClient.CurrentHttpClient.PatchAsync($"{httpClient.ConnectionString}{API_DOMAIN}/update-user/{userData}", JsonContent.Create(userData));

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
