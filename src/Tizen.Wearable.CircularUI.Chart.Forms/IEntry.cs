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

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// IEntry is an interface to describe Entry.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface IEntry
    {
        /// <summary>
        /// Gets or sets a key.
        /// Key represents the position of a chart. if the key is 3, this value set 3rd data in a chart.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        int Key { get; set; }

        /// <summary>
        /// Gets or sets a value.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        double Value { get; set; }

        /// <summary>
        /// Gets or sets a label of entry.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        Label ValueLabel { get; set; }
    }
}
