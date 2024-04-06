using MediatR;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Dialogs.ViewModels;
using TeamTaskClient.UI.Main;
using TeamTaskClient.UI.Modules.Profile.View;

namespace TeamTaskClientUI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMediator mediator, IRemoveCash removeCash)
        {
            InitializeComponent();
            DataContext = new MainWindowVM(this, mediator, removeCash);
            
            frameLayuot.NavigationService.Navigate(new ProfilePage(mediator, removeCash));

        }
    }
}