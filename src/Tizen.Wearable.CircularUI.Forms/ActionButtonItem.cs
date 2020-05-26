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
    /// The ActionButtonItem is a class that presents a menu item and associates it with a command
    /// </summary>
    [Obsolete("ActionButtonItem is obsolete as of version 1.5.0. Please use Xamarin.Forms.Button with with Button.Style(TizenSpecific) or ContentButton instead.")]
    public class ActionButtonItem : MenuItem
    {
        /// <summary>
        /// BindableProperty. Identifies the IsEnable bindable property.
        /// </summary>
        public static readonly BindableProperty IsEnableProperty = BindableProperty.Create(nameof(IsEnable), typeof(bool), typeof(ActionButtonItem), true);

        /// <summary>
        /// BindableProperty. Identifies the IsVisible bindable property.
        /// </summary>
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ActionButtonItem), true);

        /// <summary>
        /// BindableProperty. Identifies the BackgroundColor bindable property.
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ActionButtonItem), Color.Default);

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this ActionButtonItem is enabled.
        /// </summary>
        public bool IsEnable
        {
            get => (bool)GetValue(IsEnableProperty);
            set => SetValue(IsEnableProperty, value);
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this ActionButtonItem is visible.
        /// </summary>
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets a background color value of ActionButtonItem.
        /// </summary>
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
    }
}
