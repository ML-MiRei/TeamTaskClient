using System.Windows;
using System.Windows.Controls;
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


            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
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
