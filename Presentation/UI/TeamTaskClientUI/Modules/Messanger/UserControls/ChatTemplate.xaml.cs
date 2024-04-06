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
        public static DependencyProperty NameChatProperty = DependencyProperty.Register(nameof(NameChat), typeof(string), typeof(ChatTemplate));
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

        public string NameChat
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
