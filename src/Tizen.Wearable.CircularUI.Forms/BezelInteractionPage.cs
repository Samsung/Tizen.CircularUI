/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The RotaryInteractionPage is a ContentPage, which allows you to interact with Bezel rotation
    /// </summary>
    public class BezelInteractionPage : ContentPage, IBezelInteractionRouter
    {
        /// <summary>
        /// BindableProperty. Identifies the RotaryFocusObject bindable property Key.
        /// </summary>
        public static readonly BindableProperty RotaryFocusObjectProperty = BindableProperty.Create(nameof(RotaryFocusObject), typeof(IRotaryFocusable), typeof(BezelInteractionPage), null);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Appeared { get; set; }

        /// <summary>
        /// Gets or sets object of RotaryFocusObject to receive bezel action(take a rotary event) from the current page.
        /// </summary>
        public IRotaryFocusable RotaryFocusObject
        {
            get => (IRotaryFocusable)GetValue(RotaryFocusObjectProperty);
            set => SetValue(RotaryFocusObjectProperty, value);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Appeared = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Appeared = false;
        }
    }
}
