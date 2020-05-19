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
    /// This class is for the internal use by toast.
    /// </summary>
    internal class ToastProxy : IToast
    {
        IToast _toastProxy;

        public ToastProxy()
        {
            _toastProxy = DependencyService.Get<IToast>(DependencyFetchTarget.NewInstance);

            if (_toastProxy == null)
                throw new InvalidOperationException("Internal instance via DependecyService was not created.");
        }

        /// <summary>
        /// Gets or sets duration of the Toast pop-up.
        /// </summary>
        public int Duration
        {
            get
            {
                return _toastProxy.Duration;
            }

            set
            {
                _toastProxy.Duration = value;
            }
        }

        /// <summary>
        /// Gets or sets text of the Toast pop-up.
        /// </summary>
        public string Text
        {
            get
            {
                return _toastProxy.Text;
            }

            set
            {
                _toastProxy.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets icon of the Toast pop-up.
        /// </summary>
        public FileImageSource Icon
        {
            get
            {
                return _toastProxy.Icon;
            }

            set
            {
                _toastProxy.Icon = value;
            }
        }

        /// <summary>
        /// Dismisses the Toast pop-up.
        /// </summary>
        public void Dismiss()
        {
            _toastProxy.Dismiss();
        }

        /// <summary>
        /// Shows the Toast pop-up.
        /// </summary>
        public void Show()
        {
            _toastProxy.Show();
        }
    }
}
