using MediatR;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Modules.Messanger.UserControls;
using TeamTaskClient.UI.Modules.Messanger.ViewModels;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для ChatsListUC.xaml
    /// </summary>
    public partial class ChatsListPage : Page
    {
        ChatsListPageVM chatListPageVM;

        public ChatsListPage(IMediator mediator, IMessengerEvents messengerEvents)
        {
            InitializeComponent();
            chatListPageVM = new ChatsListPageVM(mediator, messengerEvents);
            DataContext = chatListPageVM;
            MessengerStorage.ChatsRefresh += MessengerStorage_ChatsRefresh;
        }

        private void MessengerStorage_ChatsRefresh(object? sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() => ChatList.Items.Refresh());
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
            catch (Exception) { }
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
