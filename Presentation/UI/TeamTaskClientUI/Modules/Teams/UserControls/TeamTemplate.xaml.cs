using System.Windows;
using System.Windows.Controls;
using TeamTaskClient.ApplicationLayer.Models;

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

    }
}
