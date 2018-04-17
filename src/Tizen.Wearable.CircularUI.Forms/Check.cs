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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The check widget allows for toggling a value between true and false.
    /// The check is extension of Xamarin.Forms.Switch.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// <example>
    /// <code>
    /// var check = new Check
    /// {
    ///     HorizontalOptions = LayoutOptions.Center,
    ///     VerticalOptions = LayoutOptions.Center,
    ///     DisplayStyle = "Onoff"
    /// }
    /// </code>
    /// </example>
    public class Check : Switch
    {
        /// <summary>
        /// BindableProperty. Identifies the DisplayStyle bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty DisplayStyleProperty = BindableProperty.Create(nameof(DisplayStyle), typeof(CheckDisplayStyle), typeof(Check), defaultValue: CheckDisplayStyle.Default);

        /// <summary>
        /// Gets or sets display style of Check.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public CheckDisplayStyle DisplayStyle
        {
            get { return (CheckDisplayStyle)GetValue(DisplayStyleProperty); }
            set { SetValue(DisplayStyleProperty, value); }
        }
    }
}
