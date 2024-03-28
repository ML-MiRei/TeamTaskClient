using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI;
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
                System.Windows.Application.Current.Windows.OfType<ErrorWindow>().First().DialogResult = false;
            }
        }

    }
}
