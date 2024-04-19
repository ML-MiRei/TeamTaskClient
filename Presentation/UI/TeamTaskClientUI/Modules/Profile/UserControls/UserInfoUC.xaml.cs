using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TeamTaskClient.UI.Modules.Profile.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserInfoUC.xaml
    /// </summary>
    public partial class UserInfoUC : UserControl
    {
        public UserInfoUC()
        {
            InitializeComponent();
        }

        public static DependencyProperty EditProfileProperty = DependencyProperty.Register("EditProfile", typeof(ICommand), typeof(UserInfoUC));

        public ICommand EditProfile
        {
            get { return (ICommand)GetValue(EditProfileProperty); }
            set { SetValue(EditProfileProperty, value); }
        }
    }
}
