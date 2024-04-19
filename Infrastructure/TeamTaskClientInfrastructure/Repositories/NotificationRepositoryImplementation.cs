using System.Net.Http.Json;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class NotificationRepositoryImplementation(IHttpClient client) : INotificationRepository
    {
        public Task DeleteNotification(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<NotificationModel>> GetNotificationsByUserId(int id)
        {
            var httpReply = await client.CurrentHttpClient.GetAsync($"{client.ConnectionString}/Notification/list");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return await httpReply.Content.ReadFromJsonAsync<List<NotificationModel>>();
            }
            throw new ConnectionException();
        }
    }
}
