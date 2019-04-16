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

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// A struct that has a latitude and longitude.
    /// </summary>
    public struct LatLng
    {
        /// <summary>
        /// Constructor a new LatLng structure
        /// </summary>
        public LatLng(double latitude, double longitude)
        {
            Latitude = Math.Min(Math.Max(latitude, -90.0), 90.0);
            Longitude = Math.Min(Math.Max(longitude, -180.0), 180.0);
        }

        /// <summary>
        /// Gets or sets a latitude degrees.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Gets or sets a longitude degrees.
        /// </summary>
        public double Longitude { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (obj.GetType() != GetType())
                return false;
            var other = (LatLng)obj;
            return Latitude == other.Latitude && Longitude == other.Longitude;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LatLng left, LatLng right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LatLng left, LatLng right)
        {
            return !Equals(left, right);
        }
    }
}
