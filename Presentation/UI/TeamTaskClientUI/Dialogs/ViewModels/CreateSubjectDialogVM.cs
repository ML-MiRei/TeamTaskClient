using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;

namespace TeamTaskClient.UI.Dialogs.ViewModels
{
    internal class CreateSubjectDialogVM : ViewModelBase
    {
        public class CreatingElement
        {
            public string Title { get; set; }
            public string Text { get; set; }
        }

        public CreateSubjectDialogVM(string textDialog, List<string> creatingPropertiesNames)
        {
            TextDialog = textDialog;
            CreatingProperties = creatingPropertiesNames.Select(p => new CreatingElement { Title = p, Text = "" }).ToList();
            Create = new CreateCommand(this);
        }




        public string TextDialog { get; set; }
        public List<CreatingElement> CreatingProperties
        {
            get;
            set;
        }



        public ICommand CloseWindow { get; } = new CloseWindowCommand();
        public ICommand Create { get; }



        private class CreateCommand(CreateSubjectDialogVM vM) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                bool canClose = true;

                for (int i = 0; i < vM.CreatingProperties.Count; i++)
                {

                    if (vM.CreatingProperties[i].Text == "")
                    {
                        canClose = false;
                        break;
                    }
                }

                if (canClose)
                    System.Windows.Application.Current.Windows.OfType<CreateSubjectDialogWindow>().First().DialogResult = true;
                else
                {
                    ErrorWindow.Show("Enter all the data");
                }


            }
        }

        private class CloseWindowCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                System.Windows.Application.Current.Windows.OfType<CreateSubjectDialogWindow>().First().DialogResult = false;
            }
        }
    }
}
