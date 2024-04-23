using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Infrastructure.LocalDB.Context;
using TeamTaskClient.Infrastructure.Repositories;
using TeamTaskClient.Infrastructure.ServerClients.Connections;
using TeamTaskClient.Infrastructure.ServerClients.Implementation;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;

namespace TeamTaskClient.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnection)
        {

            services.AddDbContext<SqlLiteDbContext>(opt =>
            {
                opt.UseSqlite(dbConnection);
            });

            services.AddSingleton<IHttpClient, TeamTaskSeverHttpClient>();

            services.AddScoped<SqlLiteDbContext>(prov =>
                prov.GetService<SqlLiteDbContext>()
            );
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
                                                .AddTransient<INotificationHubClient, NotificationHubClient>();
            return services;
        }
    }
}
