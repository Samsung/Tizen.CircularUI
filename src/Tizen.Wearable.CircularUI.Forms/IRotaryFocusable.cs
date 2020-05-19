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

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The IRotaryFocusable is an interface to take a Rotary Event
    /// </summary>
    public interface IRotaryFocusable
    {
    }

    /// <summary>
    /// The IRotaryEventReceiver is a receiver interface to take Rotary Events
    /// </summary>
    public interface IRotaryEventReceiver : IRotaryFocusable
    {
        /// <summary>
        /// Rotate it by the RotaryEventArgs value.
        /// </summary>
        /// <param name="args">Rotated direction of Rotary device</param>
        void Rotate(RotaryEventArgs args);
    }
}
