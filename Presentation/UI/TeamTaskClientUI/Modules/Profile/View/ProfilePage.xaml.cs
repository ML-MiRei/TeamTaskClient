using MediatR;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Modules.Profile.UserControls;
using TeamTaskClient.UI.Modules.Profile.ViewModels;

namespace TeamTaskClient.UI.Modules.Profile.View
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        ProfileVM vm;

        public ProfilePage(IMediator mediator, INotificationCash notificationCash )
        {
            vm = new ProfileVM(mediator, notificationCash);
            InitializeComponent();
            DataContext = vm;
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            vm.DeleteNotification((NotificationModel)((NotificationTemplate)sender).DataContext);
        }
    }
}
