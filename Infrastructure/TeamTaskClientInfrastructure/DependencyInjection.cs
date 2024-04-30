using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Infrastructure.Repositories;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.ServerClients.Implementation;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;

namespace TeamTaskClient.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, int userId, string userTag)
        {

            services.AddSingleton<IHttpClient, TeamTaskSeverHttpClient>();

            services.AddTransient<IChatRepository, ChatRepositoryImplementation>()
                                                .AddTransient<IMessageRepository, MessageRepositoryImplementation>()

                                                .AddTransient<IProjectRepository, ProjectRepositoryImplementation>()
                                                .AddTransient<ISprintRepositoryInterface, SprintRepositoryImplementation>()
                                                .AddTransient<IProjectTaskRepository, ProjectTaskRepositoryImplementation>()

                                                .AddTransient<IUserRepository, UserRepositoryImplementation>()
                                                .AddTransient<ITeamRepository, TeamRepositoryImplementation>()
                                                .AddTransient<INotificationRepository, NotificationRepositoryImplementation>()

                                                .AddTransient<IAuthorizationService, AuthorizationService>()

                                                .AddTransient<IHttpClient, TeamTaskSeverHttpClient>()

                                                .AddSingleton<IChatHubConnection, ChatHubConnection>()
                                                .AddSingleton<IMessengerEvents>(m => new ChatHubClient(m.GetService<IChatHubConnection>(), userId, userTag));
            return services;
        }
    }
}
