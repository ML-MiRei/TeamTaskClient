using Microsoft.AspNetCore.SignalR.Client;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class MessageRepositoryImplementation : IMessageRepository
    {

        private static ChatService _chatService;
        private static IHttpClient _httpClient;



        public MessageRepositoryImplementation(IHttpClient httpClient)
        {
            _chatService = ChatService.GetInstance();
            _httpClient = httpClient;
        }


        public async void CreateMessage(MessageDTO messageData)
        {


            _chatService.HubClient.SendAsync("SendMessage", messageData.UserID, messageData.ChatID, messageData.Text).Wait();

        }

        public async Task DeleteMessage(int chatId, int messageId)
        {

            _chatService.HubClient.SendAsync("DeleteMessage", chatId, messageId).Wait();
        }

        public async Task UpdateMessage(MessageEntity messageData)
        {


            _chatService.HubClient.SendAsync("UpdateMessage", messageData.ID, messageData.TextMessage).Wait();

            //var content = JsonContent.Create(messageData);

            //var httpReply = await _httpClient.CurrentHttpClient
            //         .PatchAsync($"{_httpClient.ConnectionString}/Chat/update-message/chat-id={messageData.ChatId}&message-id={messageData.ID}", content);

            //if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{
            //    throw new NotFoundException();
            //}
            //else if (httpReply.IsSuccessStatusCode)
            //{
            //    return;
            //}
            //throw new ConnectionException();
        }
    }
}
