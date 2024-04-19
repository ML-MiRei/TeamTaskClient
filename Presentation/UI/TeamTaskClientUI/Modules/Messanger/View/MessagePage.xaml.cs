using MediatR;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Messanger.Storage;
using TeamTaskClient.UI.Modules.Messanger.UserControls;
using TeamTaskClient.UI.Modules.Messanger.ViewModels;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {

        MessagesPageVM vm;

        public MessagePage(IMediator mediator)
        {
            InitializeComponent();
            vm = new MessagesPageVM(mediator);
            DataContext = vm;


            MessengerStorage.SelectedChatChanged += OnSelectedChatChanged;

            ((INotifyCollectionChanged)ListMessages.Items).CollectionChanged += ChatMessagePage_CollectionChanged;

        }

        private void OnSelectedChatChanged(object? sender, ChatModel e)
        {
            if(MessengerStorage.SelectedChat == null)
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

        private void OnSelectedChatChanged(object? sender, EventArgs e)
        {
            InputPanel.Visibility = Visibility.Visible;
        }

        private void ChatMessagePage_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            //мб убрать первое условие
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
