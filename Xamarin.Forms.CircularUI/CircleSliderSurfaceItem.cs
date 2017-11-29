using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    public class CircleSliderSurfaceItem : CircleSurfaceItem, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty. Identifies the Minimum bindable property.
        /// </summary>
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(CircleSliderSurfaceItem), 0d, coerceValue: (bindable, value) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            slider.Value = slider.Value.Clamp((double)value, slider.Maximum);
            return value;
        });

        /// <summary>
        /// BindableProperty. Identifies the Maximum bindable property.
        /// </summary>
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(CircleSliderSurfaceItem), 1d, coerceValue: (bindable, value) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            slider.Value = slider.Value.Clamp(slider.Minimum, (double)value);
            return value;
        });

        /// <summary>
        /// BindableProperty. Identifies the Step bindable property.
        /// </summary>
        public static readonly BindableProperty IncrementProperty = BindableProperty.Create(nameof(Increment), typeof(double), typeof(CircleSliderSurfaceItem), 1d, coerceValue: (bindable, value) =>
        {
            if ((double)value < 0d)
            {
                value = 1d;
            }
            return value;
        });

        /// <summary>
        /// BindableProperty. Identifies the Value bindable property.
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(CircleSliderSurfaceItem), 0d, coerceValue: (bindable, value) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            return ((double)value).Clamp(slider.Minimum, slider.Maximum);
        });

        /// <summary>
        /// Gets or sets the minimum value of the slider.
        /// </summary>
        public double Minimum { get => (double)GetValue(MinimumProperty); set => SetValue(MinimumProperty, value); }

        /// <summary>
        /// Gets or sets the maximum value of the slider.
        /// </summary>
        public double Maximum { get => (double)GetValue(MaximumProperty); set => SetValue(MaximumProperty, value); }

        /// <summary>
        /// Gets or sets the value of the slider.
        /// </summary>
        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Increment value.
        /// The value of the slider increase/decreased by the step value.
        /// </summary>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
    }
}
