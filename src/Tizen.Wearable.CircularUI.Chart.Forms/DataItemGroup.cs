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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// DataItemGroup is a set of DataItem to be displayed in a chart.
    /// </summary>
    public class DataItemGroup : INotifyPropertyChanged
    {
        IList<IDataItem> _dataItems;
        Color _color = Color.Default;
        string _label;

        public DataItemGroup() 
        {
            _dataItems = new List<IDataItem>();
        }

        public DataItemGroup(IList<IDataItem> items, string label = null)
        {
            DataItems = items;
            Label = label;
        }

        public DataItemGroup(IList<double> values, string label = null)
        {
            DataItems = new List<IDataItem>();
            Label = label;
            for (int i = 0; i < values.Count; i++)
            {
                var item = new DataItem(values[i]);
                DataItems.Add(item);
            }
        }

        /// <summary>
        /// Gets or sets a list of DataItem.
        /// </summary>
        public IList<IDataItem> DataItems
        {
            get
            {
                return _dataItems;
            }
            set
            {
                if (_dataItems == value) return;
                _dataItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a color of data set.
        /// If a chart is BarChartView, it set a whole bar color. If a chart is LineChartView, It set a line color.
        /// </summary>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color == value) return;
                _color = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a string value of data set.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged();
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
