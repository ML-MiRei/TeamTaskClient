using MediatR;
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

        }


        private void TeamTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                vm.SettingsTeam((TeamModel)(((TeamTemplate)sender).DataContext));
            TeamList.Items.Refresh();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TeamList.ItemsSource = vm.Teams.Where(t => t.TeamName.Contains(vm.InputSearchString.Trim()));
        }

        private void TeamTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((TeamModel)(((TeamTemplate)sender).DataContext)).UserRole == (int)UserRole.LEAD)
                Cursor = Cursors.Hand;
        }

        private void TeamTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
