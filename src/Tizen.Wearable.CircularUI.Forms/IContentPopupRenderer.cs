/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Threading.Tasks;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// Base interface for ContentPopup renderer.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface IContentPopupRenderer : IDisposable
    {
        /// <summary>
        /// Sets the Element associated with this renderer.
        /// </summary>
        /// <param name="element">New element.</param>
        void SetElement(ContentPopup element);

        /// <summary>
        /// Open a popup.
        /// </summary>
        /// <returns>Returns a Task with the dismiss result of the popup.</returns>
        Task Open();
    }
}
