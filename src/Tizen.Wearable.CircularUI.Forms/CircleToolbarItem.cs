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
    /// The CircleToolbarItem is a class that extends Xamarin.Forms.ToolbarItem for Circular UI.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleToolbarItem : ToolbarItem
    {
        /// <summary>
        /// BindableProperty. Identifies the Subtext bindable property to display on the menu item.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty SubTextProperty = BindableProperty.Create(nameof(SubText), typeof(string), typeof(CircleToolbarItem), null);

        /// <summary>
        /// Gets or sets Subtext to display on the menu item
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string SubText
        {
            get => (string)GetValue(SubTextProperty);
            set => SetValue(SubTextProperty, value);
        }
    }
}