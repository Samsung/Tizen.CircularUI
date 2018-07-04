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
    /// The Radio is a widget that allows one or more options to be displayed and have the user choose only one of them.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// <example>
    /// <code>
    /// var radio1 = new Radio
    /// {
    ///     GroupName = "SoundMode",
    ///     IsSelected = true,
    ///     Value = "sound"
    /// }
    ///
    /// var radio2 = new Radio
    /// {
    ///     GroupName = "SoundMode",
    ///     IsSelected = false,
    ///     Value = "vibrate"
    /// }
    ///
    /// radio1.Selected += (s,e) =>
    /// {
    ///     Console.WriteLine($"radio1 selected! value:{e.Value}");
    /// };
    ///
    /// radio2.Selected += (s,e) =>
    /// {
    ///      Console.WriteLine($"radio2 selected! value:{e.Value}");
    /// };
    /// </code>
    /// </example>
    public class Radio : View
    {
        /// <summary>
        /// BindableProperty. Identifies the Value bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(string), typeof(Radio), default(string));

        /// <summary>
        /// BindableProperty. Identifies the IsSelected bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create("IsSelected", typeof(bool), typeof(Radio), false,
             propertyChanged: IsSelectedPropertyChanged);

        /// <summary>
        /// BindableProperty. Identifies the GroupName bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create("GroupName", typeof(string), typeof(Radio), default(string));


        /// <summary>
        /// Gets or sets the value of the Radio.
        /// This is a bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name that specifies which Radio controls are mutually exclusive.
        /// It can be set to null.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this Radio is selected.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Occurs when the Radio selection was changed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler<SelectedEventArgs> Selected;


        static void IsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radioButton = (Radio)bindable;
            radioButton.Selected?.Invoke(radioButton, new SelectedEventArgs(radioButton.Value, (bool)newValue));
        }
    }
}
