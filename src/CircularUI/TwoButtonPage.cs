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

namespace CircularUI
{
    /// <summary>
    /// The TwoButtonPage is a page that has a rectangular area inside the circle as contents area. It also has two buttons and a Title area.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class TwoButtonPage : ContentPage
    {
        /// <summary>
        /// BindableProperty. Identifies the FirstButton bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty FirstButtonProperty = BindableProperty.Create(nameof(FirstButton), typeof(MenuItem), typeof(TwoButtonPage),
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });
        /// <summary>
        /// BindableProperty. Identifies the SecondButton bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty SecondButtonProperty = BindableProperty.Create(nameof(SecondButton), typeof(MenuItem), typeof(TwoButtonPage),
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });

        /// <summary>
        /// Gets or sets left button of TwoButtonPage
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public MenuItem FirstButton
        {
            get => (MenuItem)GetValue(FirstButtonProperty);
            set => SetValue(FirstButtonProperty, value);
        }
        /// <summary>
        /// Gets or sets right button of TwoButtonPage
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public MenuItem SecondButton
        {
            get => (MenuItem)GetValue(SecondButtonProperty);
            set => SetValue(SecondButtonProperty, value);
        }
    }
}
