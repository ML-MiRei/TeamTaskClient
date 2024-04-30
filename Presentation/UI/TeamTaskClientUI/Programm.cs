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

            if (Properties.Settings.Default.userId == 0)
            {
                LoginWindow = new LoginWindow(new AuthorizationService());

                if (!LoginWindow.ShowDialog().Value)
                {
                    return;
                }

            }


            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddApplication();
                    services.AddInfrastructure(Properties.Settings.Default.userId, Properties.Settings.Default.userTag);
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();


            IHttpClient httpClient = host.Services.GetService<IHttpClient>();



            var canConnection = httpClient.TryConnection(Properties.Settings.Default.userId);
            if (canConnection)
            {
                TeamHubClient teamHubClient = TeamHubClient.GetInstance(Properties.Settings.Default.userId, Properties.Settings.Default.userTag);
                ProjectHubClient projectHubClient = ProjectHubClient.GetInstance(Properties.Settings.Default.userId, Properties.Settings.Default.userTag);
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
