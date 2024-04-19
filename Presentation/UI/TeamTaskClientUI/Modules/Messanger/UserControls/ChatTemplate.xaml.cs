using System.Windows;
using System.Windows.Controls;

namespace TeamTaskClient.UI.Modules.Messanger.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ChatTemplate.xaml
    /// </summary>
    public partial class ChatTemplate : UserControl
    {
        public ChatTemplate()
        {
            InitializeComponent();
        }


        public static DependencyProperty CreatorLastMessageProperty = DependencyProperty.Register(nameof(CreatorLastMessage), typeof(string), typeof(ChatTemplate));
        public static DependencyProperty ChatNameProperty = DependencyProperty.Register(nameof(ChatName), typeof(string), typeof(ChatTemplate));
        public static DependencyProperty LastMessageProperty = DependencyProperty.Register(nameof(LastMessage), typeof(string), typeof(ChatTemplate));
        public static DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image), typeof(string), typeof(ChatTemplate));


        public string CreatorLastMessage
        {
            get
            {
                return (string)GetValue(CreatorLastMessageProperty);
            }
            set
            {
                SetValue(CreatorLastMessageProperty, value);
            }
        }

        public string ChatName
        {
            get
            {
                return (string)GetValue(CreatorLastMessageProperty);
            }
            set
            {
                SetValue(CreatorLastMessageProperty, value);
            }
        }
        public string LastMessage
        {
            get
            {
                return (string)GetValue(LastMessageProperty);
            }
            set
            {
                SetValue(LastMessageProperty, value);
            }
        }
        public string Image
        {
            get
            {
                return (string)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }
    }
}
