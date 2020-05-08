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

using System.Collections.Generic;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// DataItemGroup is a set of DataItem to be displayed in a chart.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class DataItemGroup
    {
        public DataItemGroup() 
        {
        }
        public DataItemGroup(IList<IDataItem> items, string label = null)
        {
            DataItems = items;
            Label = label;
        }

        /// <summary>
        /// Gets or sets a list of DataItem.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public IList<IDataItem> DataItems { get; set; }

        /// <summary>
        /// Gets or sets a color of data set.
        /// If a chart is BarChartView, it set a whole bar color. If a chart is LineChartView, It set a line color.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// Gets or sets a string value of data set.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Label { get; set; }
    }
}
