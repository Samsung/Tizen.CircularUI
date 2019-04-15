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

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewPins1 : ContentPage
    {
        private static double Latitude = 40.7157961;
        private static double Longitude = -74.0252194;

        public TCMapViewPins1()
        {
            InitializeComponent ();

            var option = new GoogleMapOption();
            var position = new Position(Latitude, Longitude);
            option.Center = position;
            option.Zoom = 12;
            mapview.SetMapOption(option);

            var pin1 = new Pin
            {
                Position = new Position(40.711493, -74.011351),
                Label = "Westfield World Trade Center",
                Address = "185 Greenwich St, New York, NY 10007",
                Type = PinType.SavedPin
            };

            var pin2 = new Pin
            {
                Position = new Position(40.689651, -74.045412),
                Label = "Statue of Liberty National Monument",
                Address = "New York, NY 10004",
                Type = PinType.SavedPin
            };

            var pin3 = new Pin
            {
                Position = new Position(40.748368, -73.985560),
                Label = "Empire State Building",
                Address = "20 W 34th St, New York, NY 10001",
                Type = PinType.SavedPin
            };

            mapview.Pins.Add(pin1);
            mapview.Pins.Add(pin2);
            mapview.Pins.Add(pin3);

        }
    }
}