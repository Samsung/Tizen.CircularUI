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
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The ITwoButtonPopup is an interface to describe confirmation pop-up which has circular two button, title, text, and content area
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    internal interface ITwoButtonPopup
    {
        /// <summary>
        /// Occurs when the Back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        event EventHandler BackButtonPressed;

        /// <summary>
        /// Gets or sets content view of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        View Content { get; set; }

        /// <summary>
        /// Gets or sets left button of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        MenuItem FirstButton { get; set; }
        /// <summary>
        /// Gets or sets right button of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        MenuItem SecondButton { get; set; }

        /// <summary>
        /// Gets or sets title of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets text of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        string Text { get; set; }

        /// <summary>
        /// Shows the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Show();
        /// <summary>
        /// Dismisses the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Dismiss();
    }
}