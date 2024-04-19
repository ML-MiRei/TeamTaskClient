using System.Windows;
using System.Windows.Controls;

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


        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ProjectTaskTemplate));
        public static DependencyProperty ExecutorNameProperty = DependencyProperty.Register("ExecutorName", typeof(string), typeof(ProjectTaskTemplate));
        public static DependencyProperty DetailsProperty = DependencyProperty.Register("Details", typeof(string), typeof(ProjectTaskTemplate));



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public string ExecutorName
        {
            get { return (string)GetValue(ExecutorNameProperty); }
            set { SetValue(ExecutorNameProperty, value); }
        }

        public string Details
        {
            get { return ((string)GetValue(DetailsProperty)).ToString(); }
            set { SetValue(DetailsProperty, value); }
        }

    }
}