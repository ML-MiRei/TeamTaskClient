using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.UI.Common.Base;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class ProjectPageVM : ViewModelBase
    {
        public string WatermarkText { get => "Project name.."; }

        private static ProjectPageVM _instance;
        public static ProjectPageVM Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProjectPageVM();
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

        private ProjectPageVM()
        {
            InputSearchString = WatermarkText;
        }

        public ICommand SearchProject { get; }
    }
}
