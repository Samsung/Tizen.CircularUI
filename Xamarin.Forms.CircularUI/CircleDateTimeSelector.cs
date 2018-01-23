using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// a view that can change the value by Bezel Action by touching each item of "Year: Month: Day" and "Hour: Minute: AM / PM"
    /// </summary>
    public class CircleDateTimeSelector : Xamarin.Forms.View, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty type of Marker color moving in Bezel Action
        /// </summary>
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleDateTimeSelector), Color.Default);
        /// <summary>
        /// BindableProperty type of Value
        /// </summary>
        public static readonly BindableProperty ValueTypeProperty = BindableProperty.Create(nameof(ValueType), typeof(DateTimeType), typeof(CircleDateTimeSelector), DateTimeType.Date);
        /// <summary>
        /// BindableProperty type of DateTime
        /// </summary>
        public static readonly BindableProperty DateTimeProperty = BindableProperty.Create(nameof(DateTime), typeof(DateTime), typeof(CircleDateTimeSelector), DateTime.Now);
        /// <summary>
        /// BindableProperty type of MaximumDate
        /// </summary>
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(CircleDateTimeSelector), DateTime.Now + TimeSpan.FromDays(3650));
        /// <summary>
        /// BindableProperty type of MinimumDate
        /// </summary>
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(CircleDateTimeSelector), new DateTime(1900, 1, 1));

        /// <summary>
        /// Marker color moving in Bezel Action
        /// </summary>
        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
        /// <summary>
        /// If it is Time, the UI will be changed to enable time selection. If it is a Date, the date can be changed.
        /// </summary>
        public DateTimeType ValueType { get => (DateTimeType)GetValue(ValueTypeProperty); set => SetValue(ValueTypeProperty, value); }
        /// <summary>
        /// Gets or sets the date / time.
        /// </summary>
        public DateTime DateTime { get => (DateTime)GetValue(DateTimeProperty); set => SetValue(DateTimeProperty, value); }
        /// <summary>
        /// Gets or sets the maximum date when ValueType is Date.
        /// </summary>
        public DateTime MaximumDate { get => (DateTime)GetValue(MaximumDateProperty); set => SetValue(MaximumDateProperty, value); }
        /// <summary>
        /// Gets or sets the minimum date when ValueType is Date.
        /// </summary>
        public DateTime MinimumDate { get => (DateTime)GetValue(MinimumDateProperty); set => SetValue(MinimumDateProperty, value); }
    }
}
