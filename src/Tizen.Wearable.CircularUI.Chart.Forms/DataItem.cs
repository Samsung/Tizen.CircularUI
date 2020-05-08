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
    /// The DataItem class represents a single data in the chart.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class DataItem : IDataItem
    {
        public DataItem()
        {
        }

        public DataItem(int key, double value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Gets or sets a key.
        /// Key represents the position of a chart. if the key is 3, this value set 3rd data in a chart.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public int Key { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets a color of each DataItem.
        /// If DataItem.Color is not set, the color is set DataItemGroup.Color.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// Gets or sets a value text of DataItem.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public TextItem ValueText { get; set; }
    }
}
