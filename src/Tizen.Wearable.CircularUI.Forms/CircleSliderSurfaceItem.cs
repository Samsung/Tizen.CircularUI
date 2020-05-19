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

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleSliderSurfaceItem displays circular slider at CirclePage.
    /// </summary>
    public class CircleSliderSurfaceItem : CircleSurfaceItem, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty. Identifies the Minimum bindable property.
        /// </summary>
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(CircleSliderSurfaceItem), 0d, coerceValue: (bindable, v) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            slider.Value = slider.Value.Clamp((double)v, slider.Maximum);
            return v;
        });

        /// <summary>
        /// BindableProperty. Identifies the Maximum bindable property.
        /// </summary>
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(CircleSliderSurfaceItem), 11d, coerceValue: (bindable, v) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            slider.Value = slider.Value.Clamp(slider.Minimum, (double)v);
            return v;
        });

        /// <summary>
        /// BindableProperty. Identifies the Increment bindable property.
        /// </summary>
        public static readonly BindableProperty IncrementProperty = BindableProperty.Create(nameof(Increment), typeof(double), typeof(CircleSliderSurfaceItem), 1d, coerceValue: (bindable, v) =>
        {
            if ((double)v < 0d)
            {
                v = 1d;
            }
            return v;
        });

        /// <summary>
        /// BindableProperty. Identifies the Value bindable property.
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(CircleSliderSurfaceItem), 0d, coerceValue: (bindable, v) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            return ((double)v).Clamp(slider.Minimum, slider.Maximum);
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
        /// The value of the slider is increased/decreased by the Increment value.
        /// </summary>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
    }
}
