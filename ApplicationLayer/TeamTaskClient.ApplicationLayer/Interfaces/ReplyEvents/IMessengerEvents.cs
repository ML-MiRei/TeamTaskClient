using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents
{
    public interface IMessengerEvents
    {


         event EventHandler<MessageModel> MessageReceived;
         event EventHandler MessageDeleted;
         event EventHandler<MessageEntity> MessageUpdated;


         event EventHandler<ChatModel> PrivateChatCreated;
         event EventHandler<ChatModel> GroupChatCreated;
         event EventHandler<ChatModel> ChatUpdated;
         event EventHandler<UserModel> AddNewUserChat;
         event EventHandler<string> UserFromChatDeleted;
         event EventHandler<int> ChatDeleted;

         event EventHandler<NotificationModel> NotificationAdded;
    }
}
