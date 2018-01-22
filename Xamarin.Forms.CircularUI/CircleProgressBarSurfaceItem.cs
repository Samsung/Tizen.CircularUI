using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The CircleProgressBarSurfaceItem displays circular progressbar at CirclePage.
    /// </summary>
    public class CircleProgressBarSurfaceItem : CircleSurfaceItem
    {
        /// <summary>
        /// BindableProperty. Identifies the Value bindable property.
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(CircleProgressBarSurfaceItem), 0.0 , coerceValue:CoerceValue);

        /// <summary>
        /// Gets or sets the value of the progressbar.
        /// </summary>
        public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        internal static object CoerceValue(BindableObject bindable, object value)
        {
            return ((double)value).Clamp(0.0, 1.0);
        }
    }
}
