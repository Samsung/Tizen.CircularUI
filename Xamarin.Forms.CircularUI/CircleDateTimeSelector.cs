using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    public class CircleDateTimeSelector : Xamarin.Forms.View, IRotaryFocusable
    {
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleDateTimeSelector), Color.Default);
        public static readonly BindableProperty ValueTypeProperty = BindableProperty.Create(nameof(ValueType), typeof(DateTimeType), typeof(CircleDateTimeSelector), DateTimeType.Date);
        public static readonly BindableProperty DateTimeProperty = BindableProperty.Create(nameof(DateTime), typeof(DateTime), typeof(CircleDateTimeSelector), DateTime.Now);
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(CircleDateTimeSelector), DateTime.Now + TimeSpan.FromDays(3650));
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(CircleDateTimeSelector), new DateTime(1900, 1, 1));

        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
        public DateTimeType ValueType { get => (DateTimeType)GetValue(ValueTypeProperty); set => SetValue(ValueTypeProperty, value); }
        public DateTime DateTime { get => (DateTime)GetValue(DateTimeProperty); set => SetValue(DateTimeProperty, value); }
        public DateTime MaximumDate { get => (DateTime)GetValue(MaximumDateProperty); set => SetValue(MaximumDateProperty, value); }
        public DateTime MinimumDate { get => (DateTime)GetValue(MinimumDateProperty); set => SetValue(MinimumDateProperty, value); }
    }
}
