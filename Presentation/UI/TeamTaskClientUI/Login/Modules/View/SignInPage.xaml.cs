using System.Windows.Controls;
using TeamTaskClient.Infrastructure.Services.Implementation;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Login.Modules.ViewModels;

namespace TeamTaskClient.UI.Login.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage(IAuthorizationService authorizationService)
        {
            InitializeComponent();
            DataContext = SigninPageVM.GetInstance(authorizationService);
        }

    }
}
