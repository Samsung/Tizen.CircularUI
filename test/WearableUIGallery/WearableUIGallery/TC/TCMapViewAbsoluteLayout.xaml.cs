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

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewAbsoluteLayout : ContentPage
    {
        private static double Latitude = 37.512;
        private static double Longitude = 127.063;

        public TCMapViewAbsoluteLayout()
        {
            InitializeComponent ();

            var option = new GoogleMapOption();
            var position = new Position(Latitude, Longitude);
            option.Center = position;
            option.Zoom = 15;
            option.MapType = GoogleMapType.Hybrid;

            mapview.SetMapOption(option);
        }
    }
}