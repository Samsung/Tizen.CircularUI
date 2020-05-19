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
using System.Collections.Generic;
using System.Text;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// Enumeration for position type of popup
    /// </summary>
    public enum PositionOption
    {
        /// <summary>
        /// The popup appears at the bottom of the View using the Effect. The position is changed by Offset in the center of View.
        /// </summary>
        BottomOfView,

        /// <summary>
        /// In the center of the screen, move by the Offset in the Popup.
        /// </summary>
        CenterOfParent,

        /// <summary>
        /// The value of Offset is X, Y and popup is placed on the screen.
        /// </summary>
        Absolute,

        /// <summary>
        /// Set Offset.X * Window.Width, Offset.Y * Window.Height.
        /// </summary>
        Relative
    }
}
