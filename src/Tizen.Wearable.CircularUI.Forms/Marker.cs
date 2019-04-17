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
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// A struct that has a latitude and longitude.
    /// </summary>
	public class Marker : Element
    {
        /// <summary>
        /// BindableProperty. Identifies the Position bindable property.
        /// </summary>
        public static readonly BindableProperty PositionProperty = BindableProperty.Create("Position", typeof(LatLng), typeof(Marker), default(LatLng));

        /// <summary>
        /// BindableProperty. Identifies the Address bindable property.
        /// </summary>
        public static readonly BindableProperty AddressProperty = BindableProperty.Create("Address", typeof(string), typeof(Marker), default(string));

        /// <summary>
        /// BindableProperty. Identifies the Description bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create("Description", typeof(string), typeof(Marker), default(string));

        /// <summary>
        /// BindableProperty. Identifies the IsPopupOpened bindable property.
        /// </summary>
        public static readonly BindableProperty IsPopupOpenedProperty = BindableProperty.Create("IsPopupOpened", typeof(bool), typeof(Marker), false);

        /// <summary>
        /// Gets or sets an address string of Marker.
        /// </summary>
        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        /// <summary>
        /// Gets or sets a label string of Marker pop-up.
        /// </summary>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a position of Marker.
        /// </summary>
        public LatLng Position
        {
            get { return (LatLng)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether Marker popup is opened.
        /// </summary>
        public bool IsPopupOpened
        {
            get { return (bool)GetValue(IsPopupOpenedProperty); }
            set { SetValue(IsPopupOpenedProperty, value); }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Marker)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Address?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ IsPopupOpened.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Marker left, Marker right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Marker left, Marker right)
        {
            return !Equals(left, right);
        }

        bool Equals(Marker other)
        {
            return string.Equals(Description, other.Description) && Equals(Position, other.Position) && string.Equals(Address, other.Address) && bool.Equals(IsPopupOpened, other.IsPopupOpened);
        }
    }
}
