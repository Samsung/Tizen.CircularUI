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
using System.Text;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CirclePageNotFoundException is an Exception class that occurs when a CircleSurface has no child.
    /// </summary>
    /// [Obsolete("CirclePageNotFoundException is obsolete as of version 1.5.0. Please do not use.")]
    public class CirclePageNotFoundException : Exception
    {
        /// <summary>
        /// Creates and initializes a new instance of the CirclePageNotFoundException class.
        /// </summary>
        public CirclePageNotFoundException() : base("Circle widget must be child of Circle Page.")
        {
        }
    }
}
