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
    /// The TextItem class represents properties of text used in the ChartView.
    /// This class only has font size and text color and string value.
    /// </summary>
    public class TextItem
    {
        public TextItem()
        {
        }

        public TextItem(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets the size of the font for the text.
        /// </summary>
        public double FontSize { get; set; } = 5;

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the text for the TextItem.
        /// </summary>
        public string Text { get; set; }
    }
}
