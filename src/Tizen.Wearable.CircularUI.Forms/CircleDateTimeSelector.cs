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
using Xamarin.Forms;


namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleDateTimeSelector is a view that can change the value by bezel action by touching each item of "Year: Month: Day" and "Hour: Minute: AM / PM"
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleDateTimeSelector : Xamarin.Forms.View, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty. Identifies the MarkerColor bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleDateTimeSelector), Color.Default);
        /// <summary>
        /// BindableProperty. Identifies the ValueType bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ValueTypeProperty = BindableProperty.Create(nameof(ValueType), typeof(DateTimeType), typeof(CircleDateTimeSelector), DateTimeType.Date);
        /// <summary>
        /// BindableProperty. Identifies the DateTime bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty DateTimeProperty = BindableProperty.Create(nameof(DateTime), typeof(DateTime), typeof(CircleDateTimeSelector), DateTime.Now);
        /// <summary>
        /// BindableProperty. Identifies the MaximumDate bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(CircleDateTimeSelector), DateTime.Now + TimeSpan.FromDays(3650));
        /// <summary>
        /// BindableProperty. Identifies the MinimumDate bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(CircleDateTimeSelector), new DateTime(1900, 1, 1));

        /// <summary>
        /// BindableProperty. Identifies the IsVisibleOfYear bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsVisibleOfYearProperty = BindableProperty.Create(nameof(IsVisibleOfYear), typeof(bool), typeof(CircleDateTimeSelector), true);
        /// <summary>
        /// BindableProperty. Identifies the IsVisibleOfMonth bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsVisibleOfMonthProperty = BindableProperty.Create(nameof(IsVisibleOfMonth), typeof(bool), typeof(CircleDateTimeSelector), true);
        /// <summary>
        /// BindableProperty. Identifies the IsVisibleOfDate bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsVisibleOfDateProperty = BindableProperty.Create(nameof(IsVisibleOfDate), typeof(bool), typeof(CircleDateTimeSelector), true);
        /// <summary>
        /// BindableProperty. Identifies the IsVisibleOfHour bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsVisibleOfHourProperty = BindableProperty.Create(nameof(IsVisibleOfHour), typeof(bool), typeof(CircleDateTimeSelector), true);
        /// <summary>
        /// BindableProperty. Identifies the IsVisibleOfMinute bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsVisibleOfMinuteProperty = BindableProperty.Create(nameof(IsVisibleOfMinute), typeof(bool), typeof(CircleDateTimeSelector), true);
        /// <summary>
        /// BindableProperty. Identifies the IsVisibleOfAmPm bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsVisibleOfAmPmProperty = BindableProperty.Create(nameof(IsVisibleOfAmPm), typeof(bool), typeof(CircleDateTimeSelector), true);

        /// <summary>
        /// Gets or sets Marker color
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
        /// <summary>
        /// Gets or sets DateTimeType value. If it is Time, the UI will be changed to enable time selection. If it is a Date, the date can be changed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public DateTimeType ValueType { get => (DateTimeType)GetValue(ValueTypeProperty); set => SetValue(ValueTypeProperty, value); }
        /// <summary>
        /// Gets or sets the date / time.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public DateTime DateTime { get => (DateTime)GetValue(DateTimeProperty); set => SetValue(DateTimeProperty, value); }
        /// <summary>
        /// Gets or sets the maximum date when ValueType is Date.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public DateTime MaximumDate { get => (DateTime)GetValue(MaximumDateProperty); set => SetValue(MaximumDateProperty, value); }
        /// <summary>
        /// Gets or sets the minimum date when ValueType is Date.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public DateTime MinimumDate { get => (DateTime)GetValue(MinimumDateProperty); set => SetValue(MinimumDateProperty, value); }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether the year field type is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisibleOfYear
        {
            get => (bool)GetValue(IsVisibleOfYearProperty);
            set => SetValue(IsVisibleOfYearProperty, value);
        }
        /// <summary>
        /// Gets or sets a boolean value that indicates whether the month field type is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisibleOfMonth
        {
            get => (bool)GetValue(IsVisibleOfMonthProperty);
            set => SetValue(IsVisibleOfMonthProperty, value);
        }
        /// <summary>
        /// Gets or sets a boolean value that indicates whether the date field type is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisibleOfDate
        {
            get => (bool)GetValue(IsVisibleOfDateProperty);
            set => SetValue(IsVisibleOfDateProperty, value);
        }
        /// <summary>
        /// Gets or sets a boolean value that indicates whether the hour field type is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisibleOfHour
        {
            get => (bool)GetValue(IsVisibleOfHourProperty);
            set => SetValue(IsVisibleOfHourProperty, value);
        }
        /// <summary>
        /// Gets or sets a boolean value that indicates whether the minute field type is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisibleOfMinute
        {
            get => (bool)GetValue(IsVisibleOfMinuteProperty);
            set => SetValue(IsVisibleOfMinuteProperty, value);
        }
        /// <summary>
        /// Gets or sets a boolean value that indicates whether the AmPm field type is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisibleOfAmPm
        {
            get => (bool)GetValue(IsVisibleOfAmPmProperty);
            set => SetValue(IsVisibleOfAmPmProperty, value);
        }
    }
}
