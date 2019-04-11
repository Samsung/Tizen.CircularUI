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
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The Geocoding API provides geocoding and reverse geocoding of addresses.
    /// Geocoding is the process of converting addresses into geographic coordinates.
    /// Reverse geocoding is the process of converting geographic coordinates into a human-readable address.
    /// </summary>
    public class Geocoding
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<string, string, Task<IEnumerable<Position>>> GetPositionsForAddressAsyncFunc;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<Position, Task<IEnumerable<string>>> GetAddressesForPositionFuncAsync;

        /// <summary>
        /// Returns the human-readable address from geographic coordinates parameter.
        /// </summary>
        /// <param name="position">Xamarin.Forms.Maps.Position object.</param>
        /// <returns>IEnumerable of human-readable address.</returns>
        public Task<IEnumerable<string>> GetAddressesForPositionAsync(Position position)
        {
            if (GetAddressesForPositionFuncAsync == null)
                throw new InvalidOperationException("You MUST call GoogleMap.Init(); prior to using it.");
            return GetAddressesForPositionFuncAsync(position);
        }

        /// <summary>
        /// Returns geographic coordinates from the human-readable address.
        /// </summary>
        /// <param name="address">human-readable address.</param>
        /// <param name="region"> The region code, specified as a ccTLD ("country code top-level domain") two-character value.</param>
        /// <returns>IEnumerable of Xamarin.Forms.Maps.Position object.</returns>
        public Task<IEnumerable<Position>> GetPositionsForAddressAsync(string address, string region = null)
        {
            if (GetPositionsForAddressAsyncFunc == null)
                throw new InvalidOperationException("You MUST call GoogleMap.Init(); prior to using it.");
            return GetPositionsForAddressAsyncFunc(address, region);
        }
    }
}
