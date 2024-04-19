using System.Windows;
using System.Windows.Controls;

namespace TeamTaskClient.UI.Modules.Messanger.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class MessageTemplate : UserControl
    {
        public MessageTemplate()
        {
            InitializeComponent();
        }

        public static DependencyProperty TextMessageProperty = DependencyProperty.Register(nameof(TextMessage), typeof(string), typeof(MessageTemplate));
        public static DependencyProperty DateCreatedProperty = DependencyProperty.Register(nameof(DateCreated), typeof(DateTime), typeof(MessageTemplate));
        public static DependencyProperty UserNameCreatorProperty = DependencyProperty.Register(nameof(UserNameCreator), typeof(string), typeof(MessageTemplate));


        public string TextMessage
        {
            get
            {
                return (string)GetValue(TextMessageProperty);
            }

            set
            {
                SetValue(TextMessageProperty, value);
            }
        }
        public DateTime DateCreated
        {
            get
            {
                return (DateTime)GetValue(DateCreatedProperty);
            }

            set
            {
                SetValue(DateCreatedProperty, value);
            }
        }
        public string UserNameCreator
        {
            get
            {
                return (string)GetValue(UserNameCreatorProperty);
            }

            set
            {
                SetValue(UserNameCreatorProperty, value);
            }
        }

    }
}
