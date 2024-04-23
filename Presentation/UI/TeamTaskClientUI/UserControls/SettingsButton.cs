using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamTaskClient.UI.Properties;

namespace TeamTaskClient.UI.UserControls
{
    internal class SettingsButton : Button
    {
        public SettingsButton()
        {
      
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/settings.png"));

            BorderThickness = (Thickness)new ThicknessConverter().ConvertFrom(0);

            Background = imageBrush;
        }
    }
}
