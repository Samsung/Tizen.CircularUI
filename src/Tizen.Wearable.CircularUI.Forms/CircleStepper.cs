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
using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleStepper is a class that extends Xamarin.Forms.Stepper for Circular UI.
    /// </summary>
    public class CircleStepper : Stepper, IRotaryFocusable, ICircleSurfaceConsumer
    {
        /// <summary>
        /// BindableProperty. Identifies the MarkerColor bindable property.
        /// </summary>
        [Obsolete("MarkerColor bindable property is obsolete as of Tizen.NET version 4.0.0 and is no longer supported")]
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleStepper), Color.Default);

        /// <summary>
        /// BindableProperty. Identifies the MarkerLineWidth bindable property.
        /// </summary>
        [Obsolete("MarkerLineWidth bindable property is obsolete as of Tizen.NET version 4.0.0 and is no longer supported")]
        public static readonly BindableProperty MarkerLineWidthProperty = BindableProperty.Create(nameof(MarkerLineWidth), typeof(int), typeof(CircleStepper), 23);

        /// <summary>
        /// BindableProperty. Identifies the LabelFormat bindable property.
        /// </summary>
        public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(CircleStepper), null);

        /// <summary>
        /// BindableProperty. Identifies the Title bindable property.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CircleStepper), null);

        /// <summary>
        /// BindableProperty. Identifies whether min/max value is wrapped or not.
        /// </summary>
        public static readonly BindableProperty IsWrapEnabledProperty = BindableProperty.Create(nameof(IsWrapEnabled), typeof(bool), typeof(CircleStepper), true);

        /// <summary>
        /// Gets or sets Marker color
        /// </summary>
        [Obsolete("MarkerColor is obsolete as of Tizen.NET version 4.0.0 and is no longer supported")]
        public Color MarkerColor
        {
            get => (Color)GetValue(MarkerColorProperty);
            set => SetValue(MarkerColorProperty, value);
        }

        /// <summary>
        /// Gets or sets length of Marker
        /// </summary>
        [Obsolete("MarkerLineWidth is obsolete as of Tizen.NET version 4.0.0 and is no longer supported")]
        public int MarkerLineWidth
        {
            get => (int)GetValue(MarkerLineWidthProperty);
            set => SetValue(MarkerLineWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets format in which Value is shown
        /// </summary>
        public string LabelFormat
        {
            get => (string)GetValue(LabelFormatProperty);
            set => SetValue(LabelFormatProperty, value);
        }

        /// <summary>
        /// Gets or sets title
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Gets or sets a status of Value is wrapped.
        /// </summary>
        public bool IsWrapEnabled
        {
            get => (bool)GetValue(IsWrapEnabledProperty);
            set => SetValue(IsWrapEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets a CircleSurfaceProvider.
        /// </summary>
        public ICircleSurfaceProvider CircleSurfaceProvider { get; set; }

        /// <summary>
        /// Occurs when the circle stepper's wheel is appeared.
        /// </summary>
        public event EventHandler WheelAppeared;

        /// <summary>
        /// Occurs when the circle stepper's wheel is disappeared.
        /// </summary>
        public event EventHandler WheelDisappeared;

        /// <summary>
        /// Internal use only, initializes a new instance of the EmbeddingControls.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendWheelAppeared()
        {
            WheelAppeared?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Internal use only, initializes a new instance of the EmbeddingControls.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendWheelDisappeared()
        {
            WheelDisappeared?.Invoke(this, EventArgs.Empty);
        }
    }
}
