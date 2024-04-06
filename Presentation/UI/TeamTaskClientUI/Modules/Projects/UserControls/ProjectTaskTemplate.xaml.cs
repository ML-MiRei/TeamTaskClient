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
using TeamTaskClient.UI.Modules.Profile.UserControls;

namespace TeamTaskClient.UI.Modules.Projects.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProjectTaskTemplate.xaml
    /// </summary>
    public partial class ProjectTaskTemplate : UserControl
    {
        public ProjectTaskTemplate()
        {
            InitializeComponent();
        }


        public static DependencyProperty TaskNameProperty = DependencyProperty.Register("TaskName", typeof(string), typeof(ProjectTaskTemplate));
        public static DependencyProperty ExecutorProperty = DependencyProperty.Register("Executor", typeof(string), typeof(ProjectTaskTemplate));
        public static DependencyProperty DetailProperty = DependencyProperty.Register("Detail", typeof(string), typeof(ProjectTaskTemplate));



        public string TaskName
        {
            get { return (string)GetValue(TaskNameProperty); }
            set { SetValue(TaskNameProperty, value); }
        }


        public string Executor
        {
            get { return (string)GetValue(ExecutorProperty); }
            set { SetValue(ExecutorProperty, value); }
        }

        public string Detail
        {
            get { return  ((string)GetValue(DetailProperty)).ToString();}
            set { SetValue(DetailProperty, value); }
        }

    }
}