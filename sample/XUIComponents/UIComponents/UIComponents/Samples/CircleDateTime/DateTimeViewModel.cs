using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Samples.CircleDateTime
{
    public class DateTimeViewModel : INotifyPropertyChanged
    {
        static DateTime _dateTime = DateTime.Now;

        public DateTime Datetime
        {
            get => _dateTime;
            set
            {
                //Console.WriteLine($"Set Datetime value : {value.ToString()}");
                if (_dateTime == value) return;
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonPressedExit { get; private set; }

        public DateTimeViewModel()
        {
            ButtonPressedExit = new Command(() =>
            {
                Console.WriteLine($"Saved and Exit Datetime:{Datetime.ToString()}");
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