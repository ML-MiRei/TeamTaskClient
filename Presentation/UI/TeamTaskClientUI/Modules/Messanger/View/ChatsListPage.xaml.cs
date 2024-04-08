using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Messanger.UserControls;
using TeamTaskClient.UI.Modules.Messanger.ViewModels;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для ChatsListUC.xaml
    /// </summary>
    public partial class ChatsListPage : Page
    {
        MessengerVM messengerVM;
        ChatsListPageVM chatListPageVM;

        public ChatsListPage(IMediator mediator)
        {
            InitializeComponent();
            chatListPageVM = new ChatsListPageVM(mediator);
            DataContext = chatListPageVM;
            messengerVM = MessengerVM.GetInstance(mediator);
            messengerVM.Chats.CollectionChanged += Chats_CollectionChanged;
        }

        private void Chats_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ChatList.Items.Refresh();
        }

        private void ChatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            messengerVM.SelectedChat = (((ListBox)sender).SelectedItem as ChatModel).ChatId;
        }

        private void ChatTemplate_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            chatListPageVM.SettingsChatOpen(((ChatTemplate)sender).DataContext as ChatModel);
        }
    }
}
