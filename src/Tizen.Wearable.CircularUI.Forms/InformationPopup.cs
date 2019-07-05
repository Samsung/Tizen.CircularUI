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

using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tizen.Wearable.CircularUI.Forms
{
    public class InformationPopup : BindableObject
    {
        /// <summary>
        /// BindableProperty. Identifies the IsProgressRunning bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsProgressRunningProperty = BindableProperty.Create(nameof(IsProgressRunning), typeof(bool), typeof(InformationPopup), false);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InformationPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(InformationPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the first button bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BottomButtonProperty = BindableProperty.Create(nameof(BottomButton), typeof(MenuItem), typeof(InformationPopup), null);

        IInformationPopup _popUp;

        static IList<IInformationPopup> _popUpList = new ObservableCollection<IInformationPopup>();

        /// <summary>
        /// Occurs when the device's back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler BackButtonPressed;

        public InformationPopup()
        {
            _popUp = DependencyService.Get<IInformationPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
            {
                throw new InvalidOperationException("Object reference not set to an instance of a Popup.");
            }

            _popUp.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };

            SetBinding(IsProgressRunningProperty, new Binding(nameof(IsProgressRunning), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(BottomButtonProperty, new Binding(nameof(BottomButton), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TitleProperty, new Binding(nameof(Title), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TextProperty, new Binding(nameof(Text), mode: BindingMode.OneWayToSource, source: _popUp));
        }

        /// <summary>
        /// Gets or sets progress visibility of the Popup.
        /// If this value is true. Popup displays circular progress and hides Title automatically.
        /// </summary>
        public bool IsProgressRunning
        {
            get { return (bool)GetValue(IsProgressRunningProperty); }
            set { SetValue(IsProgressRunningProperty, value); }
        }

        /// <summary>
        /// Gets or sets title of the Popup.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets text of the Popup.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets bottom button of the Popup.
        /// You should use only one property between Icon property and Text property because two area has the same position.
        /// </summary>
        public MenuItem BottomButton
        {
            get { return (MenuItem)GetValue(BottomButtonProperty); }
            set { SetValue(BottomButtonProperty, value); }
        }

        /// <summary>
        /// Shows the Popup.
        /// </summary>
        public void Show()
        {
            _popUp.Show();
            _popUpList.Add(_popUp);
        }

        /// <summary>
        /// Dismisses the InformationPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()
        {
            _popUp.Dismiss();
            _popUpList.Remove(_popUp);
        }
    }
}
