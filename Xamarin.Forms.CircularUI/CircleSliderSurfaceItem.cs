using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    public class CircleSliderSurfaceItem : CircleProgressBarSurfaceItem
    {
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(CircleSliderSurfaceItem));
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(CircleSliderSurfaceItem));

        public double Minimum { get => (double)GetValue(MinimumProperty); set => SetValue(MinimumProperty, value); }
        public double Maximum { get => (double)GetValue(MaximumProperty); set => SetValue(MaximumProperty, value); }
    }
}
