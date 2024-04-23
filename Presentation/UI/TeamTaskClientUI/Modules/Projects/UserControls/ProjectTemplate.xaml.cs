using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.UI.Modules.Projects.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProjectTemplate.xaml
    /// </summary>
    public partial class ProjectTemplate : UserControl
    {
        public ProjectTemplate()
        {
            InitializeComponent();
        }



        public static DependencyProperty ProjectLeaderNameProperty = 
            DependencyProperty.Register("ProjectLeaderName", typeof(string), typeof(ProjectTemplate));
        public static DependencyProperty ProjectNameProperty = 
            DependencyProperty.Register("ProjectName", typeof(string), typeof(ProjectTemplate));
        public static DependencyProperty SprintsProperty = 
            DependencyProperty.Register("Sprints", typeof(List<SprintModel>), typeof(ProjectTemplate));
        public static DependencyProperty UserRoleProperty = 
            DependencyProperty.Register("UserRole", typeof(int), typeof(ProjectTemplate));
        public static DependencyProperty AmountTasksProperty = 
            DependencyProperty.Register("AmountTasks", typeof(int), typeof(ProjectTemplate));

        public string ProjectLeaderName
        {
            get { return (string)GetValue(ProjectLeaderNameProperty); }
            set { SetValue(ProjectLeaderNameProperty, value); }
        }

        public string ProjectName
        {
            get { return (string)GetValue(ProjectNameProperty); }
            set { SetValue(ProjectNameProperty, value); }
        }

        public List<SprintModel> Sprints
        {
            get { return (List<SprintModel>)GetValue(SprintsProperty); }
            set { SetValue(SprintsProperty, value); }
        }

        public int UserRole
        {
            get { return (int)GetValue(UserRoleProperty); }
            set { SetValue(UserRoleProperty, value); }
        }
        
        public int AmountTasks
        {
            get { return (int)GetValue(AmountTasksProperty); }
            set { SetValue(AmountTasksProperty, value); }
        }



    }
}
