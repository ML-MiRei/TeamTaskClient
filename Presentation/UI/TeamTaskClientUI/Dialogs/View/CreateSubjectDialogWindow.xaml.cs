using System.Windows;
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Dialogs.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class CreateSubjectDialogWindow : Window
    {
        static CreateSubjectDialogVM vM;

        public CreateSubjectDialogWindow(string textDialog, List<string> actions)
        {
            InitializeComponent();
            vM = new CreateSubjectDialogVM(textDialog, actions);
            DataContext = vM;

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }


        public List<string> GetCreatingProperties()
        {

            List<string> properties = new List<string>();

            for (int i = 0; i < vM.CreatingProperties.Count; i++)
            {
                properties.Add(vM.CreatingProperties[i].Text);
            }

            return vM.CreatingProperties.Select(upp => upp.Text).ToList();

        }
    }
}
