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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class MessageTemplate : UserControl
    {
        public MessageTemplate()
        {
            InitializeComponent();
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(MessageTemplate));
        public static DependencyProperty DateCreatedProperty = DependencyProperty.Register(nameof(DateCreated), typeof(DateTime), typeof(MessageTemplate));
        public static DependencyProperty UserNameCreatorProperty = DependencyProperty.Register(nameof(UserNameCreator), typeof(string), typeof(MessageTemplate));


        public string Text {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
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
