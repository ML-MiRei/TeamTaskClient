namespace TeamTaskClient.Infrastructure.ServerClients.Interfaces
{
    public interface IHttpClient
    {
        public string ConnectionString { get; }
        public HttpClient CurrentHttpClient { get; }
        bool TryConnection(int userId);
    }
}
