using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.ServerClients.Implementation
{
    public class TeamTaskSeverHttpClient : IHttpClient
    {
        private static HttpClient _httpClient = new HttpClient();

        public string ConnectionString { get; set; }
        public HttpClient CurrentHttpClient => _httpClient;


        public bool TryConnection(int userId)
        {
            try
            {
                var result = _httpClient.GetAsync($"https://localhost:7130/api/try-connection");
                if (result.IsCompletedSuccessfully)
                {
                    ConnectionString = $"https://localhost:7130/{userId}/api";
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
