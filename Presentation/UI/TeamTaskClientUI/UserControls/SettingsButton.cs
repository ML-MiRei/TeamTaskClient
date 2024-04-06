using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TeamTaskClient.UI.UserControls
{
    internal class SettingsButton : Button
    {
        public SettingsButton()
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFrom("C:\\Users\\feyri\\source\\repos\\TeamTaskClient\\Presentation\\UI\\TeamTaskClientUI\\Resources\\settings.png");

            BorderThickness = (Thickness)new ThicknessConverter().ConvertFrom(0);

            Background = imageBrush;
        }
    }
}
