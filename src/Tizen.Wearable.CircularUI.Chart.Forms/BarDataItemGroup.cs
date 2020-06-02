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
    /// BarDataItemGroup is an inheritance type of DataItemGroup for the BarChartView.
    /// </summary>
    public class BarDataItemGroup : DataItemGroup
    {
        Color _bgColor;
        public BarDataItemGroup() : base()
        {
            _bgColor = Color.Transparent;
        }

        public BarDataItemGroup(IList<IDataItem> items, string label) : base(items, label)
        {
            _bgColor = Color.Transparent;
        }

        public BarDataItemGroup(IList<double> values, string label = null) : base(values, label)
        {
        }

        /// <summary>
        /// Gets or sets a color of bar background.
        /// </summary>
        public Color BarBackgroundColor
        {
            get
            {
                return _bgColor;
            }
            set
            {
                if (_bgColor == value) return;
                _bgColor = value;
                OnPropertyChanged();
            }
        }
    }
}
