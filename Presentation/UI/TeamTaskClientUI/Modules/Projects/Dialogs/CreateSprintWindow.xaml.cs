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
using System.Windows.Shapes;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Modules.Projects.UserControls;
using TeamTaskClient.UI.Modules.Projects.ViewModels;

namespace TeamTaskClient.UI.Modules.Projects.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для CreateSprintWindow.xaml
    /// </summary>
    public partial class CreateSprintWindow : Window
    {


        CreateSprintWindowVM vm;
        public CreateSprintWindow()
        {
            InitializeComponent();

            vm = new CreateSprintWindowVM();
            DataContext = vm;


            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }



        public List<ProjectTaskModel> GetSelectedTasks()
        {
            var templates = ListTasks.SelectedItems.OfType<ProjectTaskModel>().ToList();
            return templates;
        }


        public SprintModel GetSprintModel()
        {
            SprintModel model = new SprintModel
            {
                DateEnd = vm.DateEnd,
                DateStart = vm.DateStart,
                Tasks = ListTasks.SelectedItems.OfType<ProjectTaskModel>().ToList()
            };

            return model;
        }
    }
}
