using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Main;
using TeamTaskClientUI.Main;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class ProjectPageVM : ViewModelBase
    {
        private static IMediator _mediator;
        public string WatermarkText { get => "Project name.."; }

        private static ProjectPageVM _instance;
        public static ProjectPageVM Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProjectPageVM(_mediator);
                return _instance;
            }
        }


        private string _inputSearchString;
        public string InputSearchString
        {
            get { return _inputSearchString; }
            set
            {
                _inputSearchString = value;
                OnPropertyChanged(nameof(InputSearchString));
            }
        }

        private ProjectPageVM(IMediator mediator)
        {
            _mediator = mediator;
            InputSearchString = WatermarkText;

        }


        public ICommand SearchProject { get; }
        public ICommand ViewButton { get; }



        private class ViewProject : CommandBase
        {
            public override void Execute(object? parameter)
            {
                MainWindowVM.ToProjectTaskButton.Execute(parameter);
            }
        }
    }
}
