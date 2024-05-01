using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Cash
{
    public interface IMessengerCash
    {
        public ObservableCollection<ChatModel> Chats { get; set; }
        public ChatModel SelectedChat { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }

        public event EventHandler<ChatModel> SelectedChatChanged;

    }
}
