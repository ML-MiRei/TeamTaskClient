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
using TeamTaskClient.UI.Modules.Teams.UserControls;

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
        }


        public static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(string), typeof(CircleImage));

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }



    }
}
