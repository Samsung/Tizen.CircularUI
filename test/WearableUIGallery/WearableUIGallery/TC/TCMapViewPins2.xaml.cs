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

using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using System;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewPins2 : CirclePage
    {
        private static double Latitude = 40.7157961;
        private static double Longitude = -74.0252194;

        GoogleMapOption _option;
        public TCMapViewPins2()
        {
            InitializeComponent ();
            BindingContext = new TCMapViewPinItemsViewModel();
            var position = new Position(Latitude, Longitude);
            _option = new GoogleMapOption(position);
            _option.Zoom = 12;
            mapview.SetMapOption(_option);
        }

        void OnClickShowPopup(object sender, EventArgs args)
        {
            if (_option.IsPinsPopupOpened == true)
            {
                _option.IsPinsPopupOpened = false;
            }
            else
            {
                _option.IsPinsPopupOpened = true;
            }

            mapview.SetMapOption(_option);
        }
    }
}