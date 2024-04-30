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
using System.Windows.Shapes;

namespace TeamTaskClient.UI.Modules.Projects.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ChangeDataWindow.xaml
    /// </summary>
    public partial class ChangeDateWindow : Window
    {
        public ChangeDateWindow(DateTime selectedDate)
        {
            InitializeComponent();
            SelectedDate = selectedDate;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public DateTime SelectedDate {  get;
            set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult= true;
        }
    }
}
