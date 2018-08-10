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
using System.Globalization;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCircleProgressBarSurfaceItem2 : CirclePage
    {
        double _incValue = 0.02;
        double _progressValue;
        bool _startProgress;

        static Func<double,double>  convFuncs = (v) => v;

        public double Value
        {
            get => _progressValue;
            set
            {
                _progressValue = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public TCCircleProgressBarSurfaceItem2()
        {
            InitializeComponent ();

            var item1 = new CircleProgressBarSurfaceItem
            {
                BackgroundColor = Color.Blue,
                BackgroundRadius = 170,
                BackgroundLineWidth = 20,
                BackgroundAngleOffset = 90,
                BackgroundAngle = 270,
                BarColor = Color.Red,
                BarRadius = 170,
                BarLineWidth = 18,
                BarAngleOffset = 90,
                BarAngleMaximum = 180,
                BarAngleMinimum = 10,
                IsVisible = true
            };

            item1.SetBinding(CircleProgressBarSurfaceItem.ValueProperty, "Value", BindingMode.Default, new ValueConverter());
            percent.SetBinding(Label.TextProperty, "Value", BindingMode.Default, new ProgressConverter());
            item1.BindingContext = this;
            percent.BindingContext = this;

            CircleSurfaceItems.Add(item1);
            _startProgress = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _startProgress = false;
        }

        void OnClick(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if(_startProgress)
            {
                _startProgress = false;
                btn.Text = "start";
            }
            else
            {
                if (Value == 1.0) Value = 0.0;
                _startProgress = true;
                btn.Text = "stop";
                Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
                {
                    Value += _incValue;
                    Console.WriteLine($"Value:{string.Format("{0:f2}", Value)}");
                    if (Value > 1.0)
                    {
                        _startProgress = false;
                        Value = 1.0;
                        btn.Text = "start";
                    }
                    return this._startProgress;
                });
            }
        }

        class ValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => convFuncs((double)value);

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }

        class ProgressConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double percent = (double)value * 100;
                string str = $"{string.Format("{0:0}", percent)} %";
                return str;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }
    }
}