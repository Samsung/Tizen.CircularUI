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
    /// The Toast class provides properties that show simple types of messages
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// <example>
    /// <code>
    /// Toast.DisplayText("Hello World", 3000)
    /// Toast.DisplayIconText("Hello World", new FileImageSource { File = "icon.jpg" }, 3000)
    /// </code>
    /// </example>
    public static class Toast
    {
        /// <summary>
        /// It shows the simplest form of the message in the given duration.
        /// </summary>
        /// <param name="text">The body text of the toast.</param>
        /// <param name="duration">How long to display the text in milliseconds.</param>
        /// <since_tizen> 4 </since_tizen>
        public static void DisplayText(string text, int duration)
        {
            new ToastProxy
            {
                Text = text,
                Duration = duration,
            }.Show();
        }

        /// <summary>
        /// It shows the simplest form of the message in 3000 milliseconds.
        /// </summary>
        /// <param name="text">The body text of the toast.</param>
        public static void DisplayText(string text) => DisplayText(text, 3000);

        /// <summary>
        /// It shows simplest icon and text messege.
        /// </summary>
        /// <param name="text">The body text of the toast.</param>
        /// <param name="icon">The file path of the toast icon.</param>
        /// <param name="duration">How long to display the text in milliseconds.</param>
        /// <since_tizen> 4 </since_tizen>
        public static void DisplayIconText(string text, FileImageSource icon, int duration = 3000)
        {
            new ToastProxy
            {
                Text = text,
                Icon = icon,
                Duration = duration,
            }.Show();
        }
    }
}
