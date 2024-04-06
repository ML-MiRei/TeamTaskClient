using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.UI.Common.Base;

namespace TeamTaskClient.UI.Modules.Teams.ViewModels
{
    class TeamPageVM : ViewModelBase
    {
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
    }
}
