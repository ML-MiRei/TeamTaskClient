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
    /// Логика взаимодействия для AlertDialogWindow.xaml
    /// </summary>
    public partial class AlertDialogWindow : Window
    {
        public AlertDialogWindow(string textDialog, string positiveText, string negativeText)
        {
            InitializeComponent();
            DataContext = new AlertDialogVM(textDialog, positiveText, negativeText);
        }
    }
}
