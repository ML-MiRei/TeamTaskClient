using MediatR;
using System.Windows.Controls;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Modules.Profile.ViewModels;

namespace TeamTaskClient.UI.Modules.Profile.View
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        ProfileVM vm;

        public ProfilePage(IMediator mediator, IRemoveCash removeCash)
        {
            vm = new ProfileVM(mediator, removeCash);
            InitializeComponent();
            DataContext = vm;
        }

    }
}
