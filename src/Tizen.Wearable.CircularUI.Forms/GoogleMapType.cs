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
    /// Enumeration for the Google Map type of the MapView.
    /// </summary>
    public enum GoogleMapType
    {
        /// <summary>
        /// The default road map view. This is the default map type.
        /// </summary>
        Roadmap,
        /// <summary>
        /// The Google Earth satellite images.
        /// </summary>
        Satellite,
        /// <summary>
        /// The mixture of normal and satellite views.
        /// </summary>
        Hybrid,
        /// <summary>
        /// The physical map based on terrain information.
        /// </summary>
        Terrain
    }
}
