using System.Windows.Controls;
using TeamTaskClient.UI.Login.Modules.ViewModels;

namespace TeamTaskClient.UI.Login.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
            DataContext = SignupPageVM.Instance;
        }

    }
}
