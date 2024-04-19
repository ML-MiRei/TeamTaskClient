using System.Windows.Input;

namespace TeamTaskClient.UI.Common.Base
{

    internal abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

    }
}
