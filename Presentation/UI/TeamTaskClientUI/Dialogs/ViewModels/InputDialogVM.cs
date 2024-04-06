using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClientUI;
using TeamTaskClient.UI.Dialogs.View;
using System.Collections.ObjectModel;

namespace TeamTaskClient.UI.Dialogs.ViewModels
{
    internal class InputDialogVM : ViewModelBase
    {
        public class InputElement
        {
            public string Text { get; set; }
        }

        public string ButtonContent {  get; set; }


        public InputDialogVM(string textDialog, string contentButton, List<string> updatingProperties)
        {
            ButtonContent = contentButton;
            TextDialog = textDialog;
            InputValues = updatingProperties.Select(p => new InputElement { Text = p }).ToList();

            Save = new SaveCommand(this);

        }

        public string TextDialog { get; set; }
        public List<InputElement> InputValues
        {
            get;
            set;
        }


        public ICommand CloseWindow { get; } = new CloseWindowCommand();
        public ICommand Save { get; }



        private class SaveCommand(InputDialogVM vM) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<InputDialogWindow>().First().DialogResult = true;
            }
        }

        private class CloseWindowCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<InputDialogWindow>().First().DialogResult = false;
            }
        }
    }
}
