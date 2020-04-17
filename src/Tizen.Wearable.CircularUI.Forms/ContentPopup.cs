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

using System;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The ContentPopup is a Popup, which allows you to customize the View to be displayed.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ContentPopup : BindableObject
    {
        /// <summary>
        /// BindableProperty. Identifies the content bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(ContentPopup), null);

        IContentPopup _popUp;

        /// <summary>
        /// Occurs when the device's back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler BackButtonPressed;

        /// <summary>
        /// Creates and initializes a new instance of the ContentPopup class.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public ContentPopup()
        {
            _popUp = DependencyService.Get<IContentPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
                throw new InvalidOperationException("Object reference not set to an instance of a Popup.");

            _popUp.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };

            SetBinding(ContentProperty, new Binding(nameof(Content), mode: BindingMode.OneWayToSource, source: _popUp));
        }

        /// <summary>
        /// Gets or sets content view of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Shows the ContentPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Show()
        {
            _popUp.Show();
        }

        /// <summary>
        /// Dismisses the ContentPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss(object result = null)
        {
            _popUp.Dismiss();
        }
    }
}
