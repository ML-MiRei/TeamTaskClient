using MediatR;
using System.Windows.Controls;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для MessangerPage.xaml
    /// </summary>
    public partial class MessangerPage : Page
    {

        ChatsListPage chats;
        MessagePage message;

        public MessangerPage(IMediator mediator, IMessengerEvents messengerEvents, IMessengerCash messengerCash)
        {
            InitializeComponent();

            chats = new ChatsListPage(mediator, messengerEvents, messengerCash);
            message = new MessagePage(mediator, messengerEvents, messengerCash);

            ChatPageLayout.NavigationService.Navigate(chats);
            MessagePageLayout.NavigationService.Navigate(message);
        }

    }
}
