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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CircularUI
{
    /// <summary>
    /// The ContextPopupItem is a class to control items in a ContextPopup.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ContextPopupItem : INotifyPropertyChanged
    {
        string _label;

        /// <summary>
        /// Creates a ContextPopupItem with only a label.
        /// </summary>
        /// <param name="label">The label of the ContextPopupItem.</param>
        /// <since_tizen> 4 </since_tizen>
        public ContextPopupItem(string label)
        {
            _label = label;
        }

        /// <summary>
        /// Occurs when the label or an icon of a ContextPopupItem is changed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the label of a ContextPopupItem.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                if (value != _label)
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Called when a bindable property has changed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
