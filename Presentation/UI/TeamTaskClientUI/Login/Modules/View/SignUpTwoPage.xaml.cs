using System.Windows.Controls;
using TeamTaskClient.UI.Login.Modules.ViewModels;

namespace TeamTaskClient.UI.Login.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для SignUpTwoPage.xaml
    /// </summary>
    public partial class SignUpTwoPage : Page
    {
        public SignUpTwoPage()
        {
            InitializeComponent();
            DataContext = SignupPageVM.Instance;
        }
    }
}
