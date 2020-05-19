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

using System;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleListView is a view that represents Xamarin.Forms.ListView on Circular UI.
    /// You can move the list through bezel action.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleListView : ListView, IRotaryFocusable, ICircleSurfaceConsumer
    {
        /// <summary>
        /// BindableProperty. Identifies the Header, Footer cancel the Fish Eye Effect or not.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty CancelEffectProperty = BindableProperty.CreateAttached("CancelEffect", typeof(bool), typeof(CircleListView), false);

        /// <summary>
        /// BindableProperty. Identifies color of the scroll bar.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarColorProperty = BindableProperty.CreateAttached("BarColor", typeof(Color), typeof(CircleListView), Color.Default);

        /// <summary>
        /// Gets the Header, Footer cancel the Fish Eye Effect or not.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static bool GetCancelEffect(BindableObject view) => (bool)view.GetValue(CancelEffectProperty);

        /// <summary>
        /// Sets the Header, Footer cancel the Fish Eye Effect or not.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static void SetCancelEffect(BindableObject view, bool value) => view.SetValue(CancelEffectProperty, value);


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
