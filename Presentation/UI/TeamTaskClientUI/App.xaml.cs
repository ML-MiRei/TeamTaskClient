using System.Configuration;
using System.Data;
using System.Windows;
using TeamTaskClient.ApplicationLayer.CQRS.Chat.Queries.GetChats;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.Infrastructure.Repositories;
using TeamTaskClient.UI;
using Microsoft.Extensions.DependencyInjection;
using TeamTaskClient.Infrastructure.ServerClients.Implementation;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage;
using TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.SendMessage;




namespace TeamTaskClient.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        public App()
        {


       


        }
    }

}
