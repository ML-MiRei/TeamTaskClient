using MediatR;
using System.Windows.Controls;
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

        public MessangerPage(IMediator mediator, IMessengerEvents messengerEvents)
        {
            InitializeComponent();

            chats = new ChatsListPage(mediator, messengerEvents);
            message = new MessagePage(mediator, messengerEvents);

            ChatPageLayout.NavigationService.Navigate(chats);
            MessagePageLayout.NavigationService.Navigate(message);
        }

    }
}
