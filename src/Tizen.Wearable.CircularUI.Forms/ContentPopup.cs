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
using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The ContentPopup is a Popup, which allows you to customize the View to be displayed.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ContentPopup : Element
    {
        /// <summary>
        /// BindableProperty. Identifies the Content bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(ContentPopup), null, propertyChanged: (b, o, n) => ((ContentPopup)b).UpdateContent());

        /// <summary>
        /// BindableProperty. Identifies the IsShow bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(ContentPopup), false, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Occurs when the popup is dismissed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler Dismissed;

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
        /// Gets or sets the popup is opened.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// Dismisses the popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()
        {
            IsOpen = false;
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendDismissed()
        {
            Dismissed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SendBackButtonPressed()
        {
            return OnBackButtonPressed();
        }

        /// <summary>
        /// To change the default behavior of the BackButton. Default behavior is dismiss.
        /// </summary>
        /// <returns>Default is false</returns>
        protected virtual bool OnBackButtonPressed()
        {
            return false;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Content != null)
                SetInheritedBindingContext(Content, BindingContext);
        }

        void UpdateContent()
        {
            if (Content != null)
                OnChildAdded(Content);
        }
    }
}
