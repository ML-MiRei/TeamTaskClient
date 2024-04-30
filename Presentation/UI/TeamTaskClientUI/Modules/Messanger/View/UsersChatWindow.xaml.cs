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
using System.Windows.Shapes;
using TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Messanger.ViewModels;
using TeamTaskClient.UI.Modules.Teams.UserControls;

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для UsersChatWindow.xaml
    /// </summary>
    public partial class UsersChatWindow : Window
    {
        public UsersChatWindow(int chatId, IMediator mediator, IMessengerEvents messengerEvents)
        {
            InitializeComponent();
            DataContext = new UsersChatWindowVM(chatId, mediator, messengerEvents);
        }

        private void UserTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tag = ((UserModel)((UserTemplate)sender).DataContext).UserTag;
            if(tag != Properties.Settings.Default.userTag)
            {
                ((UsersChatWindowVM)DataContext).ClickUser(((UserModel)((UserTemplate)sender).DataContext).UserTag);
            }
        }

        private void UserTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((UserModel)((UserTemplate)sender).DataContext).UserTag != Properties.Settings.Default.userTag)
            {
                Cursor = Cursors.Hand;
            }
        }

        private void UserTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
