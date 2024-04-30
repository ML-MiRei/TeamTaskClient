using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.UI.Common.Base
{
    interface IDialogWindowBase
    {
        public static event EventHandler DialogClosed;
        public static event EventHandler DialogOpened;
    }
}
