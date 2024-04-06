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
    internal class SelectActionsDialogVM : ViewModelBase
    {


        public SelectActionsDialogVM(string textDialog, List<string> actions )
        {
            TextDialog = textDialog;
            Actions = actions;

        }

        public string TextDialog { get; set; }
        public List<string> Actions { get; set; }

        public static string SelectedAction {  get; set; }

        public ICommand CloseWindow { get; } = new CloseWindowCommand();
        public ICommand Continue { get; } = new ContinueCommand();



        private class ContinueCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<SelectActionsDialogWindow>().First().DialogResult = true;
            }
        }

        private class CloseWindowCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<SelectActionsDialogWindow>().First().DialogResult = false;
            }
        }
    }
}
