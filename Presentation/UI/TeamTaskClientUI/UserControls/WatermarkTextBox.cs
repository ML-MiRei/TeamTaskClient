using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TeamTaskClient.UI.UserControls
{
    public class WatermarkTextBox : TextBox
    {
        public Brush WatermarkForeground { get; set; }
        private Brush _baseForeground;
        public bool CanNull { get; set; } = false;


        private string _watermarkText;
        public string WatermarkText
        {
            get { return _watermarkText; }
            set { _watermarkText = value; }
        }


        public WatermarkTextBox()
        {

            VerticalContentAlignment = VerticalAlignment.Center;
            _baseForeground = Foreground;
        }

        public override void EndInit()
        {
            base.EndInit();
            Foreground = WatermarkForeground;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                SetErrorInput();
            }
            base.OnKeyDown(e);
        }


        protected void SetErrorInput()
        {
            if (string.IsNullOrEmpty(Text))
            {
                if (CanNull)
                {
                    Foreground = WatermarkForeground;
                }
                else
                {
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#CA5454");
                }
                Text = WatermarkText;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (Text.Equals(WatermarkText))
            {
                Foreground = _baseForeground;
                Text = string.Empty;
            }
        }


        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            SetErrorInput();
        }

    }
}
