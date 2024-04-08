using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
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


            MessengerVM.OnChatSelected += MessengerVM_OnChatSelected;

            ((INotifyCollectionChanged)ListMessages.Items).CollectionChanged += ChatMessagePage_CollectionChanged;

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                vm.SendMessage.Execute(vm);
            }
        }

        private void MessengerVM_OnChatSelected(object? sender, EventArgs e)
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
    }
}
