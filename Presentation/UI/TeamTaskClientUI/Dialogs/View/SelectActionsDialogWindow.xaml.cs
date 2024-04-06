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
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Dialogs.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class SelectActionsDialogWindow : Window
    {


        public SelectActionsDialogWindow(string textDialog, List<string> actions)
        {
            InitializeComponent();
            DataContext = new SelectActionsDialogVM(textDialog, actions);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectActionsDialogVM.SelectedAction = ((ListBox)sender).SelectedItem.ToString();
        }

        public string GetSelectedAction()
        {

            return SelectActionsDialogVM.SelectedAction;

        }

    }
}
