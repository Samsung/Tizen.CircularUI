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

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleScrollView has circular scroll bar. it can be scrolled by bezel action.
    /// The CircleScrollView is extension of Xamarin.Forms.ScrollView.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleScrollView : ScrollView, IRotaryFocusable, ICircleSurfaceConsumer
    {
        /// <summary>
        /// BindableProperty. Identifies the Header, Footer cancel the Fish Eye Effect or not.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarColorProperty = Xamarin.Forms.PlatformConfiguration.TizenSpecific.ScrollView.BarColorProperty;

        /// <summary>
        /// Gets or sets a scroll bar color value.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color BarColor
        {
            get => (Color)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        /// <summary>
        /// Gets or sets a CircleSurfaceProvider.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public ICircleSurfaceProvider CircleSurfaceProvider { get; set; }
    }
}
