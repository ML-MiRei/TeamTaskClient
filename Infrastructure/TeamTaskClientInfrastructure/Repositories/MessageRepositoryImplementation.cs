using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.DTOs;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Exceptions;
using TeamTaskClient.Infrastructure.LocalDB.Context;
using TeamTaskClient.Infrastructure.LocalDB.Models;
using TeamTaskClient.Infrastructure.ServerClients.Interfaces;
using TeamTaskClient.Infrastructure.Services.Implementation;

namespace TeamTaskClient.Infrastructure.Repositories
{
    public class MessageRepositoryImplementation(IHttpClient httpClient) : IMessageRepository
    {
        public async void CreateMessage(MessageDTO messageData)
        {

            ChatService chatService = ChatService.Instance;

            chatService.HubClient.SendAsync("Send", messageData.ChatID, messageData.UserID, messageData.Text).Wait();

            //HttpContent httpContent = JsonContent.Create(messageData.Text);

            //var httpReply = httpClient.CurrentHttpClient
            //                        .PostAsync($"{httpClient.ConnectionString}/Chat/create-message/chat-id={messageData.ChatID}&user-id={messageData.UserID}", httpContent).Result;


            //if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{
            //    throw new NotFoundException();
            //}
            //else if (httpReply.IsSuccessStatusCode)
            //{
            //    var message = await httpReply.Content.ReadFromJsonAsync<MessageModel>();
            //    return message;
            //}
            //throw new ConnectionException();
        }

        public async Task DeleteMessage(int chatId, int messageId)
        {
            var httpReply = await httpClient.CurrentHttpClient
                                        .DeleteAsync($"{httpClient.ConnectionString}/Chat/delete-message/chat-id={chatId}&message-id={messageId}");

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return;
            }
            throw new ConnectionException();
        }

        public async Task UpdateMessage(MessageDTO messageData)
        {
            HttpContent httpContent = JsonContent.Create(messageData.Text);


            var httpReply = await httpClient.CurrentHttpClient
                     .PatchAsync($"{httpClient.ConnectionString}/Chat/update-message/chat-id={messageData.ChatID}&message-id={messageData.ID}", httpContent);

            if (httpReply.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (httpReply.IsSuccessStatusCode)
            {
                return;
            }
            throw new ConnectionException();
        }
    }
}
