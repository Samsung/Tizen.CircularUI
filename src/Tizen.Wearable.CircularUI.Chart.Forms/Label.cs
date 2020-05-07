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

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// The Label class is minimum set of Xamarin.Fomrs.Label.
    /// This class only has font size and text color and string value.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class Label
    {
        public Label()
        {
        }

        public Label(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets the size of the font for the label.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double FontSize { get; set; } = 5;

        /// <summary>
        /// Gets or sets the Color for the text of this Label.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the text for the Label.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Text { get; set; }
    }
}
