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

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// Event arguments for events of RadioButton.
    /// </summary>
    public class SelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new SelectedEventArgs object that represents a change from RadioButton.
        /// </summary>
        /// <param name="value">The boolean value that checks whether the RadioButton is selected.</param>
        public SelectedEventArgs(string value, bool isSelected)
        {
            Value = value;
            IsSelected = isSelected;
        }

        /// <summary>
        /// Gets the Value for the SelectedEventArgs object.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the IsSelected for the SelectedEventArgs object.
        /// </summary>
        public bool IsSelected { get; private set; }
    }
}
