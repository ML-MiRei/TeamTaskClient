using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClientUI;
using TeamTaskClient.UI.Dialogs.View;

namespace TeamTaskClient.UI.Dialogs.ViewModels
{
    internal class AlertDialogVM : ViewModelBase
    {


        public AlertDialogVM(string textDialog, string positiveText, string negativeText)
        {
            TextDialog = textDialog;
            PositiveText = positiveText;
            NegativeText = negativeText;
        }

        public string TextDialog { get; set; }
        public string PositiveText { get; set; }
        public string NegativeText { get; set; }

        public ICommand PositiveAnswer { get; } = new PositiveAnswerCommand();
        public ICommand NegativeAnswer { get; } = new NegativeAnswerCommand();


        private class PositiveAnswerCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<AlertDialogWindow>().First().DialogResult = true;
            }
        }

        private class NegativeAnswerCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<AlertDialogWindow>().First().DialogResult = false;
            }
        }

    }
}
