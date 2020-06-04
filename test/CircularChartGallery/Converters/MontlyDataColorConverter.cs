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
using Tizen.Wearable.CircularUI.Chart.Forms;
using Xamarin.Forms;

namespace CircularUIChartGallery.Converters
{
    public class MontlyDataColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Data data = value as Data;
            if (data == null)
                return value;

            var count =  data.DataItemGroups.Count;
            count = Math.Min(count, 3);
            for (int i = 0; i < count; i++)
            {
                var dataItemGroup = data.DataItemGroups[i];
                foreach (var item in dataItemGroup.DataItems)
                {
                    if (item.Value <= 1500)
                    {
                        (item as DataItem).Color = Color.FromHex("#EF751D");
                    }
                    else if (item.Value <= 2000)
                    {
                        (item as DataItem).Color = Color.FromHex("#EBF51D");
                    }
                    else if (item.Value <= 2500)
                    {
                        (item as DataItem).Color = Color.FromHex("#72E61D");
                    }
                    else
                    {
                        (item as DataItem).Color = Color.FromHex("#41C7D1");
                    }
                }
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
