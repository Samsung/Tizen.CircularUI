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
    /// The CircleStepper is a class that extends Xamarin.Forms.Stepper for Circular UI.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleStepper : Xamarin.Forms.Stepper, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty. Identifies the MarkerColor bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleStepper), Color.Default);
        /// <summary>
        /// BindableProperty. Identifies the MarkerLineWidth bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MarkerLineWidthProperty = BindableProperty.Create(nameof(MarkerLineWidth), typeof(int), typeof(CircleStepper), 23);
        /// <summary>
        /// BindableProperty. Identifies the LabelFormat bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(CircleStepper), null);

        /// <summary>
        /// BindableProperty. Identifies the Title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CircleStepper), null);

        /// <summary>
        /// Gets or sets Marker color
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
        /// <summary>
        /// Gets or sets length of Marker
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public int MarkerLineWidth { get => (int)GetValue(MarkerLineWidthProperty); set => SetValue(MarkerLineWidthProperty, value); }
        /// <summary>
        /// Gets or sets format in which Value is shown
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string LabelFormat { get => (string)GetValue(LabelFormatProperty); set => SetValue(LabelFormatProperty, value); }
        /// <summary>
        /// Gets or sets title
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    }
}
