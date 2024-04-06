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
        public string ConnectionString => $"https://localhost:7130/api";
        public HttpClient CurrentHttpClient => _httpClient;
    }
}
