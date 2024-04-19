using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class SprintRepositoryImplementation(IHttpClient client) : ISprintRepositoryInterface
    {
        public async void ChangeDateEndSprint(int sprintId, DateTime dateEnd)
        {
            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/Sprint/{sprintId}/update-date-end", JsonContent.Create(dateEnd));

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

        public async void ChangeDateStartSprint(int sprintId, DateTime dateStart)
        {

            var httpReply = await client.CurrentHttpClient.PatchAsync($"{client.ConnectionString}/Sprint/{sprintId}/update-date-start", JsonContent.Create(dateStart));

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

        public async Task<SprintModel> CreateSprint(SprintEntity sprintEntity)
        {
            var content = JsonContent.Create(sprintEntity);

            var httpReply = client.CurrentHttpClient.PostAsync($"{client.ConnectionString}/Sprint/create", content).Result;

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                var s = await httpReply.Content.ReadFromJsonAsync<SprintModel>();
                return s;
            }
            throw new ConnectionException();
        }

        public async void DeleteSprint(int sprintId)
        {
            var httpReply = await client.CurrentHttpClient.DeleteAsync($"{client.ConnectionString}/Sprint/{sprintId}/delete");

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
