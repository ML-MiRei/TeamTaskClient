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

        public string CreatorLastMessage { get; set; }
        public string LastMessage { get; set; }
        public string Image { get; set; }
    }
}
