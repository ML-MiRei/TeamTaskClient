using MediatR;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Enums;
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

            vm.InterfaceRefresh += Vm_InterfaceRefresh;
        }

        private void Vm_InterfaceRefresh(object? sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() => TeamList.Items.Refresh());
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TeamList.ItemsSource = vm.Teams.Where(t => t.TeamName.Contains(vm.InputSearchString.Trim()));
        }
    }
}
