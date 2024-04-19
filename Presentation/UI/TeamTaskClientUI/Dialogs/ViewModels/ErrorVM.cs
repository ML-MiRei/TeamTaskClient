using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;

namespace TeamTaskClient.UI.Dialogs.ViewModels
{
    internal class ErrorVM : ViewModelBase
    {
        public ErrorVM(string textError)
        {
            TextError = textError;
        }

        public string TextError { get; set; }

        public ICommand CloseWindow { get; } = new CloseWindowCommand();


        private class CloseWindowCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                try
                {
                    App.Current.Windows.OfType<ErrorWindow>().First().DialogResult = false;
                }
                catch
                {

                    Programm.ErrorWindow.DialogResult = false;
                }
            }
        }

    }
}
