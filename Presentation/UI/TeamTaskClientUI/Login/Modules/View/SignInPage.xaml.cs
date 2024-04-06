using System.Windows.Controls;
using TeamTaskClient.UI.Login.Modules.ViewModels;

namespace TeamTaskClient.UI.Login.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            DataContext = SigninPageVM.Instance;
        }

    }
}
