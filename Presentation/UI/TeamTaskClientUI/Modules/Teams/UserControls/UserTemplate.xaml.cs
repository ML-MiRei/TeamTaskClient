using System.Windows;
using System.Windows.Controls;

namespace TeamTaskClient.UI.Modules.Teams.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserTemplate.xaml
    /// </summary>
    public partial class UserTemplate : UserControl
    {
        public UserTemplate()
        {
            InitializeComponent();
        }


        public static DependencyProperty FirstNameProperty = DependencyProperty.Register("FirstName", typeof(string), typeof(UserTemplate));
        public static DependencyProperty UserTagProperty = DependencyProperty.Register("UserTag", typeof(string), typeof(UserTemplate));
        public static DependencyProperty ColorNumberProperty = DependencyProperty.Register("ColorNumber", typeof(int), typeof(UserTemplate));



        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }
        public string UserTag
        {
            get { return (string)GetValue(UserTagProperty); }
            set { SetValue(UserTagProperty, value); }
        }

        public int ColorNumber
        {
            get => (int)GetValue(ColorNumberProperty);
            set => SetValue(ColorNumberProperty, value);
        }
    }
}
