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
    public partial class CreateSubjectDialogWindow : Window
    {
        static CreateSubjectDialogVM vM;

        public CreateSubjectDialogWindow(string textDialog, List<string> actions)
        {
            InitializeComponent();
            vM = new CreateSubjectDialogVM(textDialog, actions);
            DataContext = vM;
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
