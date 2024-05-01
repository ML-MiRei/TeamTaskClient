using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamTaskClient.ApplicationLayer;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.Infrastructure;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Login;
using TeamTaskClient.UI.Storages;
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
                    services.AddSingleton<IMessengerCash>(m => new MessengerCash(m.GetService<IMessengerEvents>()));
                    services.AddSingleton<IProjectsCash>(m => new ProjectsCash(m.GetService<IProjectsEvents>()));
                    services.AddSingleton<ITeamsCash>(m => new TeamsCash(m.GetService<ITeamsEvents>()));
                    services.AddSingleton<INotificationCash>(m => new NotificationCash(m.GetService<IMessengerEvents>(), m.GetService<IProjectsEvents>(), m.GetService<ITeamsEvents>()));
                    services.AddSingleton<MainWindow>();
                })
                .Build();


            IHttpClient httpClient = host.Services.GetService<IHttpClient>();



            var canConnection = httpClient.TryConnection(Properties.Settings.Default.userId);
            if (canConnection)
            {
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
