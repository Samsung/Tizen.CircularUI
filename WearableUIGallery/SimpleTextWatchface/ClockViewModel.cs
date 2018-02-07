using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleTextWatchface
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        DateTime _time;

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time != value) return;
                _time = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
