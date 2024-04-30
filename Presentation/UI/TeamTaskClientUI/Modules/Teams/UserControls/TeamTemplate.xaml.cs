using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Profile.View;
using TeamTaskClient.UI.Modules.Teams.View;
using TeamTaskClient.UI.Modules.Teams.ViewModels;

namespace TeamTaskClient.UI.Modules.Teams.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TeamTemplate.xaml
    /// </summary>
    public partial class TeamTemplate : UserControl
    {
        public TeamTemplate()
        {
            InitializeComponent();
        }

        public static DependencyProperty TeamLeadNameProperty = DependencyProperty.Register("TeamLeadName", typeof(string), typeof(TeamTemplate));
        public static DependencyProperty TeamNameProperty = DependencyProperty.Register("TeamName", typeof(string), typeof(TeamTemplate));
        public static DependencyProperty UsersProperty = DependencyProperty.Register("Users", typeof(List<UserModel>), typeof(TeamTemplate));

        public string TeamLeadName
        {
            get { return (string)GetValue(TeamLeadNameProperty); }
            set { SetValue(TeamLeadNameProperty, value); }
        }

        public string TeamName
        {
            get { return (string)GetValue(TeamNameProperty); }
            set { SetValue(TeamNameProperty, value); }
        }


        public List<UserModel> Users
        {
            get { return (List<UserModel>)GetValue(UsersProperty); }
            set { SetValue(UsersProperty, value); }
        }


        private void UserTemplate_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var user = (UserModel)((UserTemplate)sender).DataContext;

            UserInfoWindow userInfo = new UserInfoWindow(user);
            userInfo.ShowDialog();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            TeamPageVM.Instance.SettingsTeam((TeamModel)DataContext);
        }

        private void UserTemplate_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void UserTemplate_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
