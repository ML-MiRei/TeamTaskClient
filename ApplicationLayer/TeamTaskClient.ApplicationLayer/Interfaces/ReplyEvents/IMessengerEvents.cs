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


         event EventHandler<MessageModel> OnMessageReceived;
         event EventHandler OnMessageDeleted;
         event EventHandler<MessageEntity> OnMessageUpdated;
         event EventHandler<ChatModel> PrivateChatCreated;
         event EventHandler<ChatModel> GroupChatCreated;
         event EventHandler<ChatModel> ChatUpdated;
         event EventHandler<UserModel> AddNewUserChat;
         event EventHandler<string> DeleteUserFromChat;
         event EventHandler<int> DeleteChat;

         event EventHandler<NotificationModel> NotificationAdded;
    }
}
