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
    /// BarDataSet is an inheritance type of DataSet for the BarChartView.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class BarDataSet : DataSet
    {
        public BarDataSet() : base()
        {
        }

        public BarDataSet(IList<IEntry> entries, string label) : base(entries, label)
        {
        }

        /// <summary>
        /// Gets or sets a color of bar background.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color BarBackgroundColor { get; set; } = Color.Default;
    }
}
