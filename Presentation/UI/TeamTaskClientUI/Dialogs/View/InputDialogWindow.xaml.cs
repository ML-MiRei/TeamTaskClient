using System.Windows;
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Dialogs.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class InputDialogWindow : Window
    {
        static InputDialogVM vM;

        public InputDialogWindow(string textDialog, string contentButton, List<string> actions)
        {
            InitializeComponent();
            vM = new InputDialogVM(textDialog, contentButton, actions);
            DataContext = vM;


            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public List<string> GetInputValue()
        {

            return vM.InputValues.Select(upp => upp.Text).ToList();

        }
    }
}
