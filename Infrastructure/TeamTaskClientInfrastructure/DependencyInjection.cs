using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Infrastructure.LocalDB.Context;
using TeamTaskClient.Infrastructure.Repositories;
using TeamTaskClient.Infrastructure.ServerClients.Implementation;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;

namespace TeamTaskClient.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            string dbConnection = configuration["DbConnection"];
            services.AddDbContext<SqlLiteDbContext>(opt =>
            {
                opt.UseSqlite(dbConnection);
            });
            services.AddScoped<SqlLiteDbContext>(prov =>
                prov.GetService<SqlLiteDbContext>()
            );
            services.AddTransient<IChatRepository, ChatRepositoryImplementation>()
                                                .AddTransient<IMessageRepository, MessageRepositoryImplementation>()
                                                .AddTransient<IProjectRepository, ProjectRepositoryImplementation>()
                                                .AddTransient<IUserRepository, UserRepositoryImplementation>()
                                                .AddTransient<ITeamRepository, TeamRepositoryImplementation>()
                                                .AddTransient<INotificationRepository, NotificationRepositoryImplementation>()
                                                .AddTransient<IProjectTaskRepository, ProjectTaskRepositoryImplementation>()
                                                .AddTransient<IAuthorizationService, AuthorizationService>()
                                                .AddTransient<IHttpClient, TeamTaskSeverHttpClient>();
            return services;
        }
    }
}
