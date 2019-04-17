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
    /// Enumeration for the Zoom control position of the GoogleMapView.
    /// </summary>
    public enum ZoomControlPosition
    {
        /// <summary>
        /// The control should be placed along the top left of the map.
        /// </summary>
        LeftTop = 0,
        /// <summary>
        /// The control should be placed along the top right of the map.
        /// </summary>
        RightTop = 1,
        /// <summary>
        /// The control should be placed along the left center of the map.
        /// </summary>
        LeftCenter = 2,
        /// <summary>
        /// The control should be placed along the right center of the map.
        /// </summary>
        RightCenter = 3,
        /// <summary>
        /// The control should be placed along the bottom left of the map
        /// </summary>
        LeftBottom = 4,
        /// <summary>
        /// The control should be placed along the bottom right of the map
        /// </summary>
        RightBottom = 5,
        /// <summary>
        /// The control should be placed along the top center of the map
        /// </summary>
        TopCenter = 6,
        /// <summary>
        /// The control should be placed along the bottom center of the map
        /// </summary>
        BottomCenter= 7
    }
}
