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
using Xamarin.Forms.Maps;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The GoogleMapOption struct used to define the properties that can be set on a MapView.
    /// </summary>
    public struct GoogleMapOption
    {
        private double _zoom;

        public GoogleMapOption(Position center, GoogleMapType type = GoogleMapType.Roadmap, int zoomLevel = 10, 
                bool visibleZoomControl = false, bool pinsPopupOpened = false, bool enableGesture = true, 
                ZoomControlPosition controlPosition = ZoomControlPosition.RightBottom )
        {
            Center = center;
            MapType = type;
            _zoom = Math.Min(Math.Max(zoomLevel, 1.0), 20.0); ;
            IsVisibleZoomControl = visibleZoomControl;
            IsPinsPopupOpened = pinsPopupOpened;
            IsEnableGestureHandle = enableGesture;
            ZoomControlPosition = controlPosition;
        }

        /// <summary>
        /// Gets or sets a Google Map type value of GoogleMapOption.
        /// </summary>
        public GoogleMapType MapType { get; set; }

        /// <summary>
        /// Gets or sets a center postion of GoogleMapOption.
        /// This value set center of MapView. But getting value not mean current center of MapView because value can't reflect user interaction.
        /// </summary>
        public Position Center { get; set; }

        /// <summary>
        /// Gets or sets a zoom level of GoogleMapOption.
        /// This value set zoom level of MapView. But getting value not mean current zoom level of MapView because value can't reflect user interaction.
        /// </summary>
        public double Zoom 
        {
            get { return _zoom; }
            set {
                if (value > 20.0)
                    _zoom = 20;
                else if(value < 1.0)
                    _zoom = 1;
                else
                    _zoom = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether zoom control is visible.
        /// </summary>
        public bool IsVisibleZoomControl { get; set; }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether zoom and pan interaction is enable.
        /// </summary>
        public bool IsEnableGestureHandle { get; set; }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether Pins popup is opened.
        /// </summary>
        public bool IsPinsPopupOpened { get; set; }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether zoom and pan interaction is enable.
        /// </summary>
        public ZoomControlPosition ZoomControlPosition { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (obj.GetType() != GetType())
                return false;
            var other = (GoogleMapOption)obj;
            if (MapType != other.MapType)
                return false;
            if (Center.Latitude != other.Center.Latitude || Center.Longitude == other.Center.Longitude)
                return false;
            if (Zoom != other.Zoom)
                return false;
            if (IsVisibleZoomControl != other.IsVisibleZoomControl)
                return false;
            if (IsEnableGestureHandle != other.IsEnableGestureHandle)
                return false;
            if (IsPinsPopupOpened != other.IsPinsPopupOpened)
                return false;
            if (ZoomControlPosition != other.ZoomControlPosition)
                return false;
            return true;
        }

        public static bool operator ==(GoogleMapOption left, GoogleMapOption right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GoogleMapOption left, GoogleMapOption right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns the values of GoogleMapOption".
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Center[{Center.Latitude},{Center.Longitude}], MapType:{MapType}, Zoom:{Zoom}, IsEnableGesture:{IsEnableGestureHandle}, IsVisibleZoomControl:{IsVisibleZoomControl}, IsPinsPopupOpened:{IsPinsPopupOpened}, ZoomControlPosition:{ZoomControlPosition}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = MapType.GetHashCode();
                hashCode = hashCode * 397 ^ Center.GetHashCode();
                hashCode = hashCode * 397 ^ Zoom.GetHashCode();
                hashCode = hashCode * 397 ^ IsVisibleZoomControl.GetHashCode();
                hashCode = hashCode * 397 ^ IsEnableGestureHandle.GetHashCode();
                hashCode = hashCode * 397 ^ IsPinsPopupOpened.GetHashCode();
                hashCode = hashCode * 397 ^ ZoomControlPosition.GetHashCode();
                return hashCode;
            }
        }
    }
}
