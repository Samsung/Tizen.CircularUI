using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Samples
{
    public class CircleProgressBarViewModel : INotifyPropertyChanged
    {
        double _progress1;
        double _progress2;
        string _progressLabel1="0 %";
        string _progressLabel2 = "0 %";

        public string ProgressLabel1
        {
            get => _progressLabel1;
            set
            {
                if (_progressLabel1 == value) return;
                _progressLabel1 = value;
                OnPropertyChanged();
            }
        }

        public string ProgressLabel2
        {
            get => _progressLabel2;
            set
            {
                if (_progressLabel2 == value) return;
                _progressLabel2 = value;
                OnPropertyChanged();
            }
        }

        public double ProgressValue1
        {
            get => _progress1;
            set
            {
                if (_progress1 == value) return;
                _progress1 = value;
                ProgressLabel1 = _progress1 * 100 + " %";
                OnPropertyChanged();
            }
        }

        public double ProgressValue2
        {
            get => _progress2;
            set
            {
                if (_progress2 == value) return;
                _progress2 = value;
                ProgressLabel2 = _progress2 * 100 + " %";
                OnPropertyChanged();
            }
        }

        public bool Playing { get; set; }

        public CircleProgressBarViewModel()
        {
            Playing = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), UpdateProgress1);
            Device.StartTimer(TimeSpan.FromMilliseconds(200), UpdateProgress2);
        }

        bool UpdateProgress1()
        {
            ProgressValue1 += 0.05;
            if (ProgressValue1 > 1.0)
            {
                ProgressValue1 = 0;
            }
            return Playing;
        }
        bool UpdateProgress2()
        {
            ProgressValue2 += 0.05;
            if (ProgressValue2 > 1.0)
            {
                ProgressValue2 = 0;
            }
            return Playing;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}