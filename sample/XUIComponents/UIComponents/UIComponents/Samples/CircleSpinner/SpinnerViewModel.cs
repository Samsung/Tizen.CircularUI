using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Samples.CircleSpinner
{
    public class SpinnerViewModel : INotifyPropertyChanged
    {
        double _value= 9.0;
        double _hour = 0;
        double _min = 0;
        double _sec = 0;

        public double Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public double Hour
        {
            get => _hour;
            set
            {
                if (_hour == value) return;
                _hour = value;
                OnPropertyChanged();
            }
        }

        public double Minute
        {
            get => _min;
            set
            {
                if (_min == value) return;
                _min = value;
                OnPropertyChanged();
            }
        }

        public double Second
        {
            get => _sec;
            set
            {
                if (_sec == value) return;
                _sec = value;
                OnPropertyChanged();
            }
        }

        public ICommand TimerButtonPressedExit { get; private set; }

        public ICommand ButtonPressedExit { get; private set; }

        public SpinnerViewModel()
        {
            ButtonPressedExit = new Command(() =>
            {
                Console.WriteLine($"Saved and Exit Value:{Value.ToString()}");
                App.Current.MainPage.Navigation.PopAsync(true);
            });

            TimerButtonPressedExit = new Command(() =>
            {
                Console.WriteLine($"Saved and Exit Hour:{Hour}, Minute:{Minute}, Second:{Second}");
                App.Current.MainPage.Navigation.PopAsync(true);
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}