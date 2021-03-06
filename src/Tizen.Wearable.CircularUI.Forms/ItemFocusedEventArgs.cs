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

using System;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// Event arguments for the ItemFocused event of CircleListView.
    /// </summary>
    public class ItemFocusedEventArgs : EventArgs
    {
        /// <summary>
        ///  Creates a new ItemFocusedEventArgs object.
        /// </summary>
        /// <param name="item">An item data of new focused item.</param>
        /// <param name="itemIndex">An index of new focused item.</param>
        public ItemFocusedEventArgs(object item, int itemIndex)
        {
            Item = item;
            ItemIndex = itemIndex;
        }

        /// <summary>
        /// Gets the data of new focused item
        /// </summary>
        public object Item { get; private set; }

        /// <summary>
        /// Gets the index of new focused item
        /// </summary>
        public int ItemIndex { get; private set; }
    }
}
