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

        public string Lit
        {
            get { return (string)GetValue(LitProperty); }
            set { SetValue(LitProperty, value); }
        }


        //public static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(string), typeof(CircleImage));

        //public string Image
        //{
        //    get { return (string)GetValue(ImageProperty); }
        //    set { SetValue(ImageProperty, value); }
        //}



    }
}
