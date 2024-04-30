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

    }
}
