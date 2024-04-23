using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class MessageRepositoryImplementation : IMessageRepository
    {

        private static HubConnection hubClient = ChatHubClient.GetInstance().HubClient;
        private static IHttpClient _httpClient;



        public MessageRepositoryImplementation(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async void CreateMessage(MessageDTO messageData)
        {


            await hubClient.SendAsync("SendMessage", messageData.UserID, messageData.ChatID, messageData.Text);

        }

        public async Task DeleteMessage(int chatId, int messageId)
        {

            hubClient.SendAsync("DeleteMessage", chatId, messageId).Wait();
        }

        public async Task UpdateMessage(MessageEntity messageData)
        {

            hubClient.SendAsync("UpdateMessage", messageData.ChatId, messageData.ID, messageData.TextMessage).Wait();
        }
    }
}
