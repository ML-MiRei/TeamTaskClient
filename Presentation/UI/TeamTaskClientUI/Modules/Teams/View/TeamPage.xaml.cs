using MediatR;
using System.Windows.Controls;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;
using TeamTaskClient.UI.Modules.Teams.ViewModels;

namespace TeamTaskClient.UI.Modules.Teams.View
{

    public partial class TeamPage : Page
    {

        TeamPageVM vm;

        public TeamPage(IMediator mediator, ITeamsCash teamsCash)
        {
            InitializeComponent();

            vm = new TeamPageVM(mediator, teamsCash);
            DataContext = vm;

            teamsCash.TeamUpdated += OnTeamUpdated;
        }

        private void OnTeamUpdated(object? sender, EventArgs e)
        {
            TeamList.Items.Refresh();
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TeamList.ItemsSource = vm.Teams.Where(t => t.TeamName.Contains(vm.InputSearchString.Trim()));
        }
    }
}
