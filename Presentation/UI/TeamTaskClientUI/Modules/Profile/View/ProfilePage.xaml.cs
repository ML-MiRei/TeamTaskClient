using MediatR;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Modules.Profile.ViewModels;

namespace TeamTaskClient.UI.Modules.Profile.View
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(IMediator mediator, IRemoveCash removeCash)
        {
            InitializeComponent();
            DataContext = new ProfileVM(mediator, removeCash);
        }
    }
}
