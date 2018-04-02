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
        String _mode = "Watch";
        bool _isNormalMode = true;

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged();
            }
        }

        public String Mode
        {
            get => _mode;
            set
            {
                if (_mode == value) return;
                _mode = value;
                OnPropertyChanged();
            }
        }

        public bool IsNormalMode
        {
            get => _isNormalMode;
            set
            {
                if (_isNormalMode == value) return;
                _isNormalMode = value;
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
