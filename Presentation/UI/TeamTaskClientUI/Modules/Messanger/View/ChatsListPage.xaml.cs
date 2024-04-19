using MediatR;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Messanger.Storage;
using TeamTaskClient.UI.Modules.Messanger.UserControls;
using TeamTaskClient.UI.Modules.Messanger.ViewModels;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для ChatsListUC.xaml
    /// </summary>
    public partial class ChatsListPage : Page
    {
        ChatsListPageVM chatListPageVM;

        public ChatsListPage(IMediator mediator)
        {
            InitializeComponent();
            chatListPageVM = new ChatsListPageVM(mediator);
            DataContext = chatListPageVM;
            MessengerStorage.SelectedChatChanged += OnSelectedChatChanged; ;
        }

        private void OnSelectedChatChanged(object? sender, ChatModel e)
        {
            ChatList.Items.Refresh();
        }

        public void Refresh()
        {
            ChatList.Items.Refresh();

        }

        private void ChatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((((ListBox)sender).SelectedItem as ChatModel) == null)
                    MessengerStorage.SelectedChat = null;
                else
                    MessengerStorage.SelectedChat = (((ListBox)sender).SelectedItem as ChatModel);
            }
            catch(Exception) { }
        }

        private void ChatTemplate_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            chatListPageVM.SettingsChatOpen(((ChatTemplate)sender).DataContext as ChatModel);
        }

        private void ChatTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ChatTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
