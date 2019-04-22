/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace WearableUIGallery.TC
{

    public class TCCirclePageViewModel : INotifyPropertyChanged
    {
        Random _random = new Random();
        double _progress;
        bool _progressBarVisibility;
        bool _alertSliderVisibility;
        bool _ringtoneSliderVisibility;
        bool _dateVisibility = true;
        IRotaryFocusable _rotaryFocusTarget;

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

        public bool DateVisiblity
        {
            get => _dateVisibility;
            set
            {
                if (_dateVisibility == value) return;
                _dateVisibility = value;
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

        public IRotaryFocusable RotaryFocusTarget
        {
            get => _rotaryFocusTarget;
            set
            {
                if (_rotaryFocusTarget == value) return;
                _rotaryFocusTarget = value;
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
        public ProgressCommand InVisible { get; private set; }

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
            InVisible = new ProgressCommand { Text = "InVisible", SubText = "InVisible DateSelector", Icon = "image/tw_ic_popup_btn_delete.png", Action = new Command(DoInVisibleDateSelector) };
            Playing = false;
            KeepGoing = false;

            ProgressBarVisibleCommand = new Command((obj) =>
            {
                var r = obj as IRotaryFocusable;
                ProgressBarVisibility = !ProgressBarVisibility;
                RotaryFocusTarget = r;
                AlertSliderVisibility = false;
                RingtoneSliderVisibility = false;
                DateVisiblity = true;
            });
        }

        void DoPlay()
        {
            Console.WriteLine("DoPlay!!");
            Playing = true;
            Progress = 0;
            KeepGoing = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000.0 / 60.0), UpdateProgress);
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
            Device.StartTimer(TimeSpan.FromMilliseconds(1000.0 / 60.0), UpdateProgress);
        }

        void DoShuffle()
        {
            Console.WriteLine("DoShuffle!!");
            Playing = true;
            KeepGoing = true;
            Progress = 0;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000.0 / 60), UpdateShuffleProgress);
        }

        void DoAlertVolume(object obj)
        {
            Console.WriteLine("DoAlertVolume!!");
            AlertSliderVisibility = true;
            RotaryFocusTarget = obj as IRotaryFocusable;
        }

        void DoRingtoneVolume(object obj)
        {
            Console.WriteLine("DoRingtoneVolume!!");
            RingtoneSliderVisibility = true;
            RotaryFocusTarget = obj as IRotaryFocusable;
        }

        void DoInVisibleDateSelector(object obj)
        {
            Console.WriteLine("DoInVisibleDateSelector!!");
            DateVisiblity = !DateVisiblity;
            RotaryFocusTarget = obj as IRotaryFocusable;
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
