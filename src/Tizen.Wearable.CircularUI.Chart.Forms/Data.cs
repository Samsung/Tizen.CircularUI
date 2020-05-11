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
    /// The Data class is a set of DataItemGroup.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class Data : INotifyPropertyChanged
    {
        IList<DataItemGroup> _dataItemGroups;

        public Data() 
        {
        }

        public Data(DataItemGroup dataItemGroup)
        {
            DataItemGroups = new List<DataItemGroup>();
            DataItemGroups.Add(dataItemGroup);
        }


        public Data(IList<DataItemGroup> dataItemGroups)
        {
            DataItemGroups = dataItemGroups;
        }

        /// <summary>
        /// Gets or sets a list of DataItemGroup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public IList<DataItemGroup> DataItemGroups
        {
            get
            {
                return _dataItemGroups;
            }
            set
            {
                if (_dataItemGroups == value) return;
                _dataItemGroups = value;
                Console.WriteLine("Data.DataItemGroups set value");
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
