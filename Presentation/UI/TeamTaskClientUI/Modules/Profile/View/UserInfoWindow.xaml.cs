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
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.UI.Modules.Profile.View
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow(UserModel userModel)
        {
            InitializeComponent();
            DataContext = userModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
