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

        public static DependencyProperty LeaderNameProperty = DependencyProperty.Register("LeaderName", typeof(string), typeof(ProjectTemplate));
        public static DependencyProperty NumberOfTasksProperty = DependencyProperty.Register("NumberOfTasks", typeof(string), typeof(ProjectTemplate));
        public static DependencyProperty SprintProperty = DependencyProperty.Register("Sprint", typeof(string), typeof(ProjectTemplate));
        public static DependencyProperty MyRoleProperty = DependencyProperty.Register("MyRole", typeof(string), typeof(ProjectTemplate));
        public static DependencyProperty ViewButtonProperty = DependencyProperty.Register("ViewButton", typeof(ICommand), typeof(ProjectTemplate));



        public string LeaderName
        {
            get { return (string)GetValue(LeaderNameProperty); }
            set { SetValue(LeaderNameProperty, value); }
        }

        public string NumberOfTasks
        {
            get { return (string)GetValue(NumberOfTasksProperty); }
            set { SetValue(NumberOfTasksProperty, value); }
        }

        
        public string Sprint
        {
            get { return (string)GetValue(SprintProperty); }
            set { SetValue(SprintProperty, value); }
        }

                
        public string MyRole
        {
            get { return (string)GetValue(MyRoleProperty); }
            set { SetValue(MyRoleProperty, value); }
        }



        public ICommand ViewButton
        {
            get { return (ICommand)GetValue(ViewButtonProperty); }
            set { SetValue(ViewButtonProperty, value);}
        }


    }
}
