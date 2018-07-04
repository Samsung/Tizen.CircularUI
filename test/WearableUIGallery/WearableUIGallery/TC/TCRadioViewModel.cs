using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace WearableUIGallery.TC
{
    public class MyCustomData : INotifyPropertyChanged
    {
        bool _checked;

        public MyCustomData()
        {
        }

        public string Text { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }

        public bool IsSelected
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TCRadioViewModel : INotifyPropertyChanged
    {

        public IList<MyCustomData> SampleData { get; set; } = new ObservableCollection<MyCustomData>();

        string _radioLabel1 = "Sound";
        string _radioLabel2 = "Medium";

        public string RadioLabel1
        {
            get => _radioLabel1;
            set
            {
                if (_radioLabel1 == value) return;
                _radioLabel1 = value;
                OnPropertyChanged();
            }
        }

        public string RadioLabel2
        {
            get => _radioLabel2;
            set
            {
                if (_radioLabel2 == value) return;
                _radioLabel2 = value;
                OnPropertyChanged();
            }
        }

        public MyCustomData Sound { get; private set; }
        public MyCustomData Vibrate { get; private set; }
        public MyCustomData Mute { get; private set; }
        public MyCustomData Strong { get; private set; }
        public MyCustomData Medium { get; private set; }
        public MyCustomData Weak { get; private set; }

        public TCRadioViewModel()
        {
            SampleData.Add(new MyCustomData() { Text = "No off", Value = "NoOff", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "15 seconds", Value = "15s", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "30 seconds", Value = "30s", GroupName = "timeout", IsSelected = true });
            SampleData.Add(new MyCustomData() { Text = "1 minute", Value = "1m", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "5 minute", Value = "5m", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "10 minute", Value = "10m", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "15 minute", Value = "15m", GroupName = "timeout", IsSelected = false });

            //SoundMode
            Sound = new MyCustomData() { Text = "Sound", Value = "sound", GroupName = "SoundMode", IsSelected = true };
            Vibrate = new MyCustomData() { Text = "Vibrate", Value = "vibrate", GroupName = "SoundMode", IsSelected = false };
            Mute = new MyCustomData() { Text = "Mute", Value = "mute", GroupName = "SoundMode", IsSelected = false };

            //Vibrate Strength
            Strong = new MyCustomData() { Text = "Strong", Value = "3", GroupName = "VibratorStrength", IsSelected = false };
            Medium = new MyCustomData() { Text = "Medium", Value = "2", GroupName = "VibratorStrength", IsSelected = true };
            Weak = new MyCustomData() { Text = "Weak", Value = "1", GroupName = "VibratorStrength", IsSelected = false };

            Sound.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsSelected")
                {
                    if (Sound.IsSelected) RadioLabel1 = "Sound";
                }
            };

            Vibrate.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsSelected")
                {
                    if (Vibrate.IsSelected) RadioLabel1 = "Vibrate";
                }
            };

            Mute.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsSelected")
                {
                    if (Mute.IsSelected) RadioLabel1 = "Mute";
                }
            };

            Strong.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsSelected")
                {
                    if (Strong.IsSelected) RadioLabel2 = "Strong";
                }
            };

            Medium.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsSelected")
                {
                    if (Medium.IsSelected) RadioLabel2 = "Medium";
                }
            };

            Weak.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsSelected")
                {
                    if (Weak.IsSelected) RadioLabel2 = "Weak";
                }
            };
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}