using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Modules.Messanger.Models;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class ChatsListPageVM : ViewModelBase
    {
        private ObservableCollection<ChatModel> _chats;
        public ObservableCollection<ChatModel> Chats
        {
            get { return _chats; }
            set { _chats = value; }
        }


    }
}
