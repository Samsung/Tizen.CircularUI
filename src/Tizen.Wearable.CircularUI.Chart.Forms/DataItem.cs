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
    public class DataItem : IDataItem
    {
        public DataItem()
        {
        }

        public DataItem(double value)
        {
            Value = value;
        }

        public DataItem(double value, TextItem label)
        {
            Value = value;
            Label = label;
        }

        /// <summary>
        /// Gets or sets a value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets a color of each DataItem.
        /// This property specifies color of each DataItem.
        /// If DataItem.Color is not set, the color is set DataItemGroup.Color.
        /// </summary>
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// Gets or sets a value label of DataItem.
        /// This property specifies the label for displaying the value of DataItem.
        /// </summary>
        public TextItem ValueLabel { get; set; }

        /// <summary>
        /// Gets or sets a label of each DataItem.
        /// This property specifies the label of category in BarCharView and LineChartView. 
        /// Category label displays data's category in major axis.
        /// This property specifies the label of Legend in PieChartView and DonutChartView.
        /// </summary>
        public TextItem Label { get; set; }
    }
}
