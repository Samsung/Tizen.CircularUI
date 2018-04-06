/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CircularUI
{
    /// <summary>
    /// The CircleSliderSurfaceItem displays circular slider at CirclePage.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleSliderSurfaceItem : CircleSurfaceItem, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty. Identifies the Minimum bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(CircleSliderSurfaceItem), 0d, coerceValue: (bindable, value) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            slider.Value = slider.Value.Clamp((double)value, slider.Maximum);
            return value;
        });

        /// <summary>
        /// BindableProperty. Identifies the Maximum bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(CircleSliderSurfaceItem), 1d, coerceValue: (bindable, value) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            slider.Value = slider.Value.Clamp(slider.Minimum, (double)value);
            return value;
        });

        /// <summary>
        /// BindableProperty. Identifies the Increment bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
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
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(CircleSliderSurfaceItem), 0d, coerceValue: (bindable, value) =>
        {
            var slider = (CircleSliderSurfaceItem)bindable;
            return ((double)value).Clamp(slider.Minimum, slider.Maximum);
        });

        /// <summary>
        /// Gets or sets the minimum value of the slider.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double Minimum { get => (double)GetValue(MinimumProperty); set => SetValue(MinimumProperty, value); }

        /// <summary>
        /// Gets or sets the maximum value of the slider.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double Maximum { get => (double)GetValue(MaximumProperty); set => SetValue(MaximumProperty, value); }

        /// <summary>
        /// Gets or sets the value of the slider.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
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
        /// <since_tizen> 4 </since_tizen>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
    }
}
