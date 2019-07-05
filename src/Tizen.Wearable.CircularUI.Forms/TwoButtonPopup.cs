﻿/*
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The TwoButtonPopup describe pop-up which has circular two button, title, text, and content area.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// <example>
    /// <code>
    /// var leftButton = new MenuItem()
    /// {
    ///     Text = "Save",
    ///     Icon = new FileImageSource{ File = "icon_save.png", },
    ///     Command = new Command(() => { ... })
    /// };
    ///
    /// var rightButton = new MenuItem()
    /// {
    ///     Text = "Delete",
    ///     Icon = new FileImageSource{ File = "icon_delete.png", },
    ///     Command = new Command(() => { ... })
    /// };
    ///
    /// var popup = new TwoButtonPopup();
    /// popup.FirstButton = leftButton;
    /// popup.SecondButton = rightButton;
    /// popup.Title = "Popup title";
    /// popup.Content = new StackLayout()
    /// {
    ///     HorizontalOptions = LayoutOptions.FillAndExpand,
    ///     Children =
    ///     {
    ///        new Label
    ///        {
    ///            Text = "Will be saved",
    ///        },
    ///     }
    /// };
    ///
    /// popup.BackButtonPressed += (s, e) =>
    /// {
    ///     popup.Dismiss();
    /// };
    /// </code>
    /// </example>
    public class TwoButtonPopup : BindableObject
    {
        /// <summary>
        /// BindableProperty. Identifies the content bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(TwoButtonPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(TwoButtonPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(TwoButtonPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the first button bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty FirstButtonProperty = BindableProperty.Create(nameof(FirstButton), typeof(MenuItem), typeof(TwoButtonPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the second button bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty SecondButtonProperty = BindableProperty.Create(nameof(SecondButton), typeof(MenuItem), typeof(TwoButtonPopup), null);

        ITwoButtonPopup _popUp;

        static IList<ITwoButtonPopup> _popUpList = new ObservableCollection<ITwoButtonPopup>();

        /// <summary>
        /// Occurs when the device's back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler BackButtonPressed;

        /// <summary>
        /// Creates and initializes a new instance of the TwoButtonPopup class.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public TwoButtonPopup()
        {
            _popUp = DependencyService.Get<ITwoButtonPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
            {
                throw new InvalidOperationException("Object reference not set to an instance of a Popup.");
            }

            _popUp.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };

            SetBinding(ContentProperty, new Binding(nameof(Content), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(FirstButtonProperty, new Binding(nameof(FirstButton), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(SecondButtonProperty, new Binding(nameof(SecondButton), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TitleProperty, new Binding(nameof(Title), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TextProperty, new Binding(nameof(Text), mode: BindingMode.OneWayToSource, source: _popUp));
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
        /// Gets or sets title of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets text of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets left button of the Popup.
        /// Text property of MenuItem is ignored since button has no space to display text.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public MenuItem FirstButton
        {
            get { return (MenuItem)GetValue(FirstButtonProperty); }
            set { SetValue(FirstButtonProperty, value); }
        }

        /// <summary>
        /// Gets or sets right button of the Popup.
        /// Text property of MenuItem is ignored since button has no space to display text.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public MenuItem SecondButton
        {
            get { return (MenuItem)GetValue(SecondButtonProperty); }
            set { SetValue(SecondButtonProperty, value); }
        }

        /// <summary>
        /// Shows the TwoButtonPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Show()
        {
            _popUp.Show();
            _popUpList.Add(_popUp);
        }

        /// <summary>
        /// Dismisses the TwoButtonPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()
        {
            _popUp.Dismiss();
            _popUpList.Remove(_popUp);
        }
    }
}
