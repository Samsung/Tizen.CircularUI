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
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCircleDateTimeSelector : IndexPage
    {
        DateTimeType type;
        LabelColorRotaryProxy colorRotaryProxy;

        public TCCircleDateTimeSelector ()
        {
            InitializeComponent ();

            colorRotaryProxy = new LabelColorRotaryProxy(SettingColorLabel);

            SettingColorLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => SettingPage.SetValue(CircleSurfaceEffectBehavior.RotaryFocusObjectProperty, colorRotaryProxy))
            });

            SettingScroller.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => SettingPage.SetValue(CircleSurfaceEffectBehavior.RotaryFocusObjectProperty, SettingScroller))
            });

            type = DateTimeType.Date;

            SettingValueTypeLabel.Text = type.ToString();
            SettingValueType.IsToggled = true;
            SettingValueType.Toggled += (s, e) =>
            {
                type = SettingValueType.IsToggled ? DateTimeType.Date : DateTimeType.Time;
                SettingValueTypeLabel.Text = type.ToString();
            };

            TimeTextPage.Appearing += (s, e) =>
            {
                StringBuilder sb = new StringBuilder();
                if (TimeSelector.ValueType == DateTimeType.Date)
                {
                    if (TimeSelector.IsVisibleOfYear)
                    {
                        sb.AppendLine($"{TimeSelector.DateTime.Year} Year");
                    }
                    if (TimeSelector.IsVisibleOfMonth)
                    {
                        sb.AppendLine($"{TimeSelector.DateTime.Month} Month");
                    }
                    if (TimeSelector.IsVisibleOfDate)
                    {
                        sb.AppendLine($"{TimeSelector.DateTime.Day} Day");
                    }
                }
                else
                {
                    if (TimeSelector.IsVisibleOfHour)
                    {
                        sb.AppendLine($"{TimeSelector.DateTime.Hour} Hour");
                    }
                    if (TimeSelector.IsVisibleOfMinute)
                    {
                        sb.AppendLine($"{TimeSelector.DateTime.Minute} Minute");
                    }
                    if (TimeSelector.IsVisibleOfAmPm)
                    {
                        sb.AppendLine($"{(TimeSelector.DateTime.Hour < 12 ? "AM" : "PM")}");
                    }
                }

                sb.AppendLine();
                sb.AppendLine($"Max = {(TimeSelector.ValueType == DateTimeType.Date ? TimeSelector.MaximumDate.ToString("yyyy-MM-dd") : TimeSelector.MaximumDate.ToString("HH:mm"))}");
                sb.AppendLine($"Min = {(TimeSelector.ValueType == DateTimeType.Date ? TimeSelector.MinimumDate.ToString("yyyy-MM-dd") : TimeSelector.MinimumDate.ToString("HH:mm"))}");

                TimeText.Text = sb.ToString();
            };
        }
    }

    class ColorHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ToHexString((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string ToHexString(Color color)
        {
            int r = (int)(color.R * 255);
            int g = (int)(color.G * 255);
            int b = (int)(color.B * 255);
            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }

    class ReverseColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            return new Color(Color.White.R - color.R, Color.White.G - color.G, Color.White.B - color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ValueTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Boolean)value ? DateTimeType.Date : DateTimeType.Time;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class LabelColorRotaryProxy : IRotaryEventReceiver
    {
        Color color;
        Label source;
        public LabelColorRotaryProxy(Label label)
        {
            source = label;
            color = Color.FromHsla(0, 1, 0.5);

            source.BackgroundColor = color;
        }

        public void Rotate(RotaryEventArgs args)
        {
            var h = (color.Hue + 0.05 * (args.IsClockwise ? 1 : -1)) % 1;
            color = Color.FromHsla(h, color.Saturation, color.Luminosity);
            source.BackgroundColor = color;
        }
    }
}