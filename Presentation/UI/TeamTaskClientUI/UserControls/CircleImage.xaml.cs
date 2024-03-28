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

namespace TeamTaskClient.UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CircleButton.xaml
    /// </summary>
    public partial class CircleImage : UserControl
    {
        public CircleImage()
        {
            InitializeComponent();
            DataContext = this;
        }


        public string _image;
        public string AvatarImage
        {
            get { return _image; }

            set { _image = value; 
            }
        }

    }
}
