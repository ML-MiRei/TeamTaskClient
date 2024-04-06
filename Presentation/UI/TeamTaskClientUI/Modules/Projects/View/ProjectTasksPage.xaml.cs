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
using TeamTaskClient.UI.Modules.Projects.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectTasksPage.xaml
    /// </summary>
    public partial class ProjectTasksPage : Page
    {
        public ProjectTasksPage(IMediator mediator)
        {
            InitializeComponent();
            DataContext = new ProjectTasksVM(mediator);
        }
    }
}
