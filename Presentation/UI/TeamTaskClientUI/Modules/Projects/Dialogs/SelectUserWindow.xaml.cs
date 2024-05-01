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
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для CreateSprintWindow.xaml
    /// </summary>
    public partial class SelectUserWindow : Window
    {


        public SelectUserWindow(IProjectsCash projectsCash)
        {
            InitializeComponent();

            DataContext = new SetExecutorWindowVM(projectsCash);

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }



        public UserModel GetExecutor()
        {
            var templates = ListUsers.SelectedItem as UserModel;
            return templates;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
