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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            set { SetValue(EditProfileProperty, value);}
        }
    }
}
