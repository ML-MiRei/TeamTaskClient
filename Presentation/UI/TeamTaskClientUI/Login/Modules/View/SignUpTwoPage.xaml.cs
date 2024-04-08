using System.Windows.Controls;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Login.Modules.ViewModels;

namespace TeamTaskClient.UI.Login.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для SignUpTwoPage.xaml
    /// </summary>
    public partial class SignUpTwoPage : Page
    {
        public SignUpTwoPage(IAuthorizationService authorizationService)
        {
            InitializeComponent();
            DataContext = SignupPageVM.GetInstance(authorizationService);
        }
    }
}
