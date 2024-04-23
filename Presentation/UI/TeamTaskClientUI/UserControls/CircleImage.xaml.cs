using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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



        public static DependencyProperty LitProperty = DependencyProperty.Register("Lit", typeof(string), typeof(CircleImage));
        public static DependencyProperty ColorNumberProperty = DependencyProperty.Register("ColorNumber", typeof(int), typeof(CircleImage));





        public string Lit
        {
            get { return (string)GetValue(LitProperty); }
            set { SetValue(LitProperty, value); }
        }

        public int ColorNumber
        {
            get { return (int)GetValue(ColorNumberProperty); }
            set { SetValue(ColorNumberProperty, value); }
        }
    }
}
