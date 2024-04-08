using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TeamTaskClientUI.Main;
using TeamTaskClient.ApplicationLayer;
using TeamTaskClient.Infrastructure;
using System.Configuration;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById;
using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserByTag;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;
using System.Windows;
using TeamTaskClient.UI.Login;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Dialogs.View;
using System.Net.NetworkInformation;
using System.Net;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.UI
{
    public class Programm
    {
        public static LoginWindow LoginWindow;
        public static ErrorWindow ErrorWindow;




        [STAThread]
        public static void Main()
        {


            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<IAuthorizationService, AuthorizationService>();
                    services.AddApplication();
                    services.AddInfrastructure(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
                })
                .Build();




            if (Properties.Settings.Default.userId == 0)
            {
                LoginWindow = new LoginWindow(host.Services.GetService<IAuthorizationService>());

                if (!LoginWindow.ShowDialog().Value)
                {
                    return;
                }

            }

            IHttpClient httpClient = host.Services.GetService<IHttpClient>();
            httpClient.TryConnection(Properties.Settings.Default.userId);

            ChatService chatService = ChatService.GetInstance(Properties.Settings.Default.userId);



            var app = host.Services.GetService<App>();




           // IMediator mediator = host.Services.GetService<IMediator>();




            app?.Run();
        }
    }
}
