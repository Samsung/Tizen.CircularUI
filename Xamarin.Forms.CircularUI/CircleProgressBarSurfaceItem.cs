using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    public class CircleProgressBarSurfaceItem : CircleSurfaceItem
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(CircleProgressBarSurfaceItem), 0.0 , coerceValue:CoerceValue);

        public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        internal static object CoerceValue(BindableObject bindable, object value)
        {
            return ((double)value).Clamp(0.0, 1.0);
        }
    }
}
