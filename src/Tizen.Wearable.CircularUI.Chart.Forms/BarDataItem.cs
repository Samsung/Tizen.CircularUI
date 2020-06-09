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

using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// BarDataItem is an inheritance type of DataItem for the BarChartView..
    /// </summary>
    public class BarDataItem : DataItem
    {
        public BarDataItem(): base()
        {
        }

        public BarDataItem(double value): base(value)
        {
        }

        public BarDataItem(double value, TextItem label) : base(value, label)
        {
        }

        /// <summary>
        /// Gets or sets a color of bar background.
        /// </summary>
        public Color BarBackgroundColor { get; set; } = Color.Transparent;

        /// <summary>
        /// Gets or sets a position of ValueLabel.
        /// </summary>
        public ValueLabelPosition ValueLabelPosition { get; set; } = ValueLabelPosition.End;
    }
}
