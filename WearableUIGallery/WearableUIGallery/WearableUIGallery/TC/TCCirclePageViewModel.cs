using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace WearableUIGallery
{
    internal static class WidgetName
    {
        public const string DateSelector = "DateSelector";
        public const string Alert = "AlertSlider";
        public const string Ringtone = "RingtoneSlider";
    }

    public class TCCirclePageViewModel : INotifyPropertyChanged
    {
        Random _random = new Random();
        double _progress;
        bool _progressBarVisibility;
        bool _alertSliderVisibility;
        bool _ringtoneSliderVisibility;
        string _rotaryFocusName = WidgetName.DateSelector;

        public double Progress
        {
            get => _progress;
            set
            {
                if (_progress == value) return;
                _progress = value;
                OnPropertyChanged();
            }
        }

        public bool ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                if (_progressBarVisibility == value) return;
                _progressBarVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool AlertSliderVisibility
        {
            get => _alertSliderVisibility;
            set
            {
                if (_alertSliderVisibility == value) return;
                _alertSliderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool RingtoneSliderVisibility
        {
            get => _ringtoneSliderVisibility;
            set
            {
                if (_ringtoneSliderVisibility == value) return;
                _ringtoneSliderVisibility = value;
                OnPropertyChanged();
            }
        }

        public string RotaryFocusName
        {
            get => _rotaryFocusName;
            set
            {
                if (_rotaryFocusName == value) return;
                _rotaryFocusName = value;
                OnPropertyChanged();
            }
        }

        public ICommand ProgressBarVisibleCommand { get; private set; }

        public ProgressCommand Play { get; private set; }
        public ProgressCommand Stop { get; private set; }
        public ProgressCommand Repeat { get; private set; }
        public ProgressCommand Shuffle { get; private set; }
        public ProgressCommand Alert { get; private set; }
        public ProgressCommand Ringtone { get; private set; }

        public bool Playing { get; set; }
        public bool KeepGoing { get; set; }

        public TCCirclePageViewModel()
        {
            Play = new ProgressCommand { Text = "Play", SubText = "Play one time", Icon = "image/music_controller_btn_play.png", Action = new Command(DoPlay) };
            Stop = new ProgressCommand { Text = "Stop", SubText = "Stop and reset", Icon = "image/music_controller_btn_stop.png", Action = new Command(DoStop) };
            Repeat = new ProgressCommand { Text = "Repeat", SubText = "Keep playing", Icon = "image/music_controller_btn_repeat_all.png", Action = new Command(DoRepeat) };
            Shuffle = new ProgressCommand { Text = "Shuffle", SubText = "Carry on Random", Icon = "image/music_controller_btn_shuffle_on.png", Action = new Command(DoShuffle) };
            Alert = new ProgressCommand { Text = "Alert", SubText = "Alert volume", Icon = "image/icon_alert_sound.png", Action = new Command(DoAlertVolume) };
            Ringtone = new ProgressCommand { Text = "Ringtone", SubText = "Ringtone volume", Icon = "image/icon_ringtone_sound.png", Action = new Command(DoRingtoneVolume) };
            Playing = false;
            KeepGoing = false;

            ProgressBarVisibleCommand = new Command(() =>
            {
                ProgressBarVisibility = !ProgressBarVisibility;
                RotaryFocusName = WidgetName.DateSelector;
                AlertSliderVisibility = false;
                RingtoneSliderVisibility = false;
            });
        }

        void DoPlay()
        {
            Console.WriteLine("DoPlay!!");
            Playing = true;
            Progress = 0;
            KeepGoing = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000 / 60), UpdateProgress);
        }

        void DoStop()
        {
            Console.WriteLine("DoStop!!");
            Playing = false;
            Progress = 0;
        }

        void DoRepeat()
        {
            Console.WriteLine("DoRepeat!!");
            Playing = true;
            KeepGoing = true;
            Progress = 0;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000 / 60), UpdateProgress);
        }

        void DoShuffle()
        {
            Console.WriteLine("DoShuffle!!");
            Playing = true;
            KeepGoing = true;
            Progress = 0;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000 / 60), UpdateShuffleProgress);
        }

        void DoAlertVolume(object obj)
        {
            Console.WriteLine("DoAlertVolume!!");
            AlertSliderVisibility = true;
            RotaryFocusName = WidgetName.Alert;
        }

        void DoRingtoneVolume(object obj)
        {
            Console.WriteLine("DoRingtoneVolume!!");
            RingtoneSliderVisibility = true;
            RotaryFocusName = WidgetName.Ringtone;
        }

        bool UpdateProgress()
        {
            Progress += 1.0 / 60.0;
            if (Progress >= 1.0)
            {
                Progress = 0;
                if (!KeepGoing) Playing = false;
            }
            return Playing;
        }

        bool UpdateShuffleProgress()
        {
            Progress = _random.NextDouble();
            return Playing;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ProgressCommand
    {
        public string Text { get; set; }
        public string SubText { get; set; }
        public string Icon { get; set; }
        public ICommand Action { get; set; }
    }
}
