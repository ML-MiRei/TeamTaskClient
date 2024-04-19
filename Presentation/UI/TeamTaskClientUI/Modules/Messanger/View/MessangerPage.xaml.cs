using MediatR;
using System.Windows.Controls;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для MessangerPage.xaml
    /// </summary>
    public partial class MessangerPage : Page
    {

        ChatsListPage chats;
        MessagePage message;

        public MessangerPage(IMediator mediator)
        {
            InitializeComponent();

            chats = new ChatsListPage(mediator);
            message = new MessagePage(mediator);

            ChatPageLayout.NavigationService.Navigate(chats);
            MessagePageLayout.NavigationService.Navigate(message);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            chats.Refresh();
        }
    }
}
