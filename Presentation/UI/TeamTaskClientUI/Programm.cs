using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamTaskClient.ApplicationLayer;
using TeamTaskClient.Infrastructure;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Login;
using TeamTaskClientUI.Main;

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



            var canConnection = httpClient.TryConnection(Properties.Settings.Default.userId);
            if (canConnection)
            {
                ChatHubClient chatService = ChatHubClient.GetInstance(Properties.Settings.Default.userId, Properties.Settings.Default.userTag);
                TeamHubClient teamHubClient = TeamHubClient.GetInstance(Properties.Settings.Default.userId, Properties.Settings.Default.userTag);
                var app = host.Services.GetService<App>();
                app?.Run();
            }
            else
            {
                ErrorWindow.Show("No connection");
            }


        }
    }
}
