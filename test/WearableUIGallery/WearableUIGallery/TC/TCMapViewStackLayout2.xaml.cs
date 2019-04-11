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
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Maps;
using System;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewStackLayout2 : ContentPage
    {
        private static double Latitude = 37.512;
        private static double Longitude = 127.063;

        MapOption _option;

        private ZoomControlPosition[] _positions = { ZoomControlPosition.LeftBottom, ZoomControlPosition.LeftCenter, ZoomControlPosition.LeftTop, ZoomControlPosition.RightTop, ZoomControlPosition.RightCenter, ZoomControlPosition.RightBottom };
        int index;

        public TCMapViewStackLayout2()
        {
            InitializeComponent ();

            var position = new Position(Latitude, Longitude);
            _option = new MapOption(position);
            _option.Zoom = 15;
            _option.MapType = GoogleMapType.Satellite;
            mapview.SetMapOption(_option);
            index = 0;
        }

        void OnClickZoomControlVisible(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Zoom invisible")
            {
                btn.Text = "Zoom visible";
                _option.IsVisibleZoomControl = true;
            }
            else
            {
                btn.Text = "Zoom invisible";
                _option.IsVisibleZoomControl = false;
            }

            mapview.SetMapOption(_option);
        }

        void OnClickZoomControlMove(object sender, EventArgs args)
        {
            _option.ZoomControlPosition = _positions[index++];
            mapview.SetMapOption(_option);
        }

        void OnClickGestureHandle(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Gesture disable")
            {
                btn.Text = "Gesture enable";
                _option.IsEnableGestureHandle = true;
            }
            else
            {
                btn.Text = "Gesture disable";
                _option.IsEnableGestureHandle = false;
            }

            mapview.SetMapOption(_option);
        }
    }
}