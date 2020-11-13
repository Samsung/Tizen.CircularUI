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
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// Class that the XAML parser uses to convert Progress to Bound.
    /// </summary>
    public class ProgressToBoundTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = (double)value;
            if (Double.IsNaN(progress))
            {
                progress = 0d;
            }
            return new Rectangle(0, 0, progress, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Rectangle rect = (Rectangle)value;
            return rect.Width;
        }
    }

    /// <summary>
    /// Class that the XAML parser uses to convert milliseconds to Text format.
    /// </summary>
    public class MillisecondToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int millisecond = (int)value;
            int second = (millisecond / 1000) % 60;
            int min = (millisecond / 1000 / 60) % 60;
            int hour = (millisecond / 1000 / 60 / 60);
            if (hour > 0)
            {
                return string.Format("{0:d2}:{1:d2}:{2:d2}", hour, min, second);
            }
            else
            {
                return string.Format("{0:d2}:{1:d2}", min, second);
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
