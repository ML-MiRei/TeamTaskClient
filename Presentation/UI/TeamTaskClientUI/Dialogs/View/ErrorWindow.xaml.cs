using System.Windows;
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Dialogs.View
{
    /// <summary>
    /// Логика взаимодействия для ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private ErrorWindow(string textError)
        {
            InitializeComponent();
            DataContext = new ErrorVM(textError);


            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public static void Show(string textError)
        {
            ErrorWindow errorWindow = new ErrorWindow(textError);
            errorWindow.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
