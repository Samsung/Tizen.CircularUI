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
    /// The CircleProgressBarSurfaceItem displays circular progressbar at CirclePage.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleProgressBarSurfaceItem : CircleSurfaceItem
    {
        /// <summary>
        /// BindableProperty. Identifies the Value bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(CircleProgressBarSurfaceItem), 0.0 , coerceValue:CoerceValue);

        /// <summary>
        /// Gets or sets the value of the progressbar.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        internal static object CoerceValue(BindableObject bindable, object value)
        {
            return ((double)value).Clamp(0.0, 1.0);
        }
    }
}
