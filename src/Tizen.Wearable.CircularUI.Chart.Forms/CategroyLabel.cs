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
    /// The CategoryLabel class represents the position and label of category.
    /// </summary>
    public class CategoryLabel
    {
        public CategoryLabel()
        {
        }

        public CategoryLabel(int key, string text)
        {
            Key = key;
            Label = new Label(text);
        }

        public CategoryLabel(int key, Label label)
        {
            Key = key;
            Label = label;
        }

        /// <summary>
        /// Gets or sets a key.
        /// Key represents the position of a chart. if the key is 3, this label is displayed 3rd position.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets a label of category.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Label Label { get; set; }
    }
}
