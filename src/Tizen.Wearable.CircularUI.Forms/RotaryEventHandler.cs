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
    /// Delegate for a triggering rotary event
    /// </summary>
    /// <param name="args">Rotated direction of Rotary device</param>
    /// <since_tizen> 4 </since_tizen>
    public delegate void RotaryEventHandler(RotaryEventArgs args);

    /// <summary>
    /// Event arguments for RotaryEvent.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class RotaryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets rotated direction of Rotary device. IsClockwise is true when Rotary device rotated in the clockwise direction or false on counter clockwise.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsClockwise { get; set; }
    }
}