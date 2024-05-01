using MediatR;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.UI.Modules.Messanger.UserControls;
using TeamTaskClient.UI.Modules.Messanger.ViewModels;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {

        MessagesPageVM vm;
        IMessengerCash _messengerCash;


        public MessagePage(IMediator mediator, IMessengerEvents messengerEvents, IMessengerCash messengerCash)
        {
            InitializeComponent();
            _messengerCash = messengerCash;

            vm = new MessagesPageVM(mediator, messengerEvents, messengerCash);
            DataContext = vm;

            vm.InterfaceRefresh += Vm_InterfaceRefresh;
            messengerCash.SelectedChatChanged += OnSelectedChatChanged;

            ((INotifyCollectionChanged)ListMessages.Items).CollectionChanged += ChatMessagePage_CollectionChanged;

        }

        private void Vm_InterfaceRefresh(object? sender, EventArgs e)
        {
           App.Current.Dispatcher.Invoke(() => ListMessages.Items.Refresh());
        }

        private void OnSelectedChatChanged(object? sender, ChatModel e)
        {
            if(_messengerCash.SelectedChat == null)
                InputPanel.Visibility = Visibility.Hidden;
            else
                InputPanel.Visibility = Visibility.Visible;

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                vm.SendMessage.Execute(vm);
            }
        }


        private void ChatMessagePage_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ListMessages.ScrollIntoView(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset && ((ItemCollection)sender).Count > 0)
            {
                ListMessages.ScrollIntoView(((ItemCollection)sender)[((ItemCollection)sender).Count - 1]);
            }



        }

        private void MessageTemplate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var message = ((MessageTemplate)sender).DataContext as MessageModel;

            if (message.CreatorTag == Properties.Settings.Default.userTag)
                vm.DoubleClick.Execute(((MessageTemplate)sender).DataContext);

        }

        private void MessageTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((((MessageTemplate)sender).DataContext as MessageModel).CreatorTag == Properties.Settings.Default.userTag)
                Cursor = Cursors.Hand;
        }

        private void MessageTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
