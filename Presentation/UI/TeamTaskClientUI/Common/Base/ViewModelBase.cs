﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TeamTaskClient.UI.Common.Base
{
    public class ViewModelBase() : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
