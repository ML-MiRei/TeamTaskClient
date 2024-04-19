using System.Windows;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Dialogs.View
{
    /// <summary>
    /// Логика взаимодействия для AlertDialogWindow.xaml
    /// </summary>
    public partial class AlertDialogWindow : Window
    {
        public AlertDialogWindow(string textDialog, string positiveText, string negativeText)
        {
            InitializeComponent();
            DataContext = new AlertDialogVM(textDialog, positiveText, negativeText);

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
