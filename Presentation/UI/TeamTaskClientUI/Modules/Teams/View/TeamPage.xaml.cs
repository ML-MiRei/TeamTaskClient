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
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Teams.UserControls;
using TeamTaskClient.UI.Modules.Teams.ViewModels;

namespace TeamTaskClient.UI.Modules.Teams.View
{
    /// <summary>
    /// Логика взаимодействия для TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {

        TeamPageVM vm;

        public TeamPage(IMediator mediator)
        {
            InitializeComponent();

            vm = new TeamPageVM(mediator); 
            DataContext = vm; 
        }

        private void TeamTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vm.SettingsTeam((TeamModel)(((TeamTemplate)sender).DataContext));
        }
    }
}
