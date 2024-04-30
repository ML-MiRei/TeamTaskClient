using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.HubClients;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class MessageRepositoryImplementation(IChatHubConnection chatHubClient) : IMessageRepository
    {

        private HubConnection hubClient = chatHubClient.HubConnection;



        public async Task CreateMessage(MessageEntity messageData)
        {
            await hubClient.SendAsync("SendMessage", messageData.CreatorID, messageData.ChatId, messageData.TextMessage);
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
