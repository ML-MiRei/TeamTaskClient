using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Modules.Messanger.Models;

namespace TeamTaskClient.UI.Modules.Messanger.ViewModels
{
    internal class MessagesPageVM : ViewModelBase
    {

        public string WatermarkText { get => "Enter message.."; }

        private static MessagesPageVM _instance;
        public static MessagesPageVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessagesPageVM();
                }
                return _instance;
            }
        }

        private MessagesPageVM() {
            InputMessage = WatermarkText;
        }


        private ObservableCollection<MessageModel> _messages;
        public ObservableCollection<MessageModel> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }


        private string _inputMessage;
        public string InputMessage
        {
            get { return _inputMessage; }
            set
            {
                _inputMessage = value;
                OnPropertyChanged(nameof(InputMessage));
            }
        }



        public ICommand SendMessage { get; }


        private class SendMessageCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                throw new NotImplementedException();
            }
        }
    }
}
