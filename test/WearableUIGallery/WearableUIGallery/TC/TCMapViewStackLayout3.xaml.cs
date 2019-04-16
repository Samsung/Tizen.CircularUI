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
using System;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewStackLayout3 : ContentPage
    {
        private static double Latitude = 37.512;
        private static double Longitude = 127.063;

        GoogleMapOption _option;

        private ZoomControlPosition[] _positions = { ZoomControlPosition.LeftBottom, ZoomControlPosition.LeftCenter, ZoomControlPosition.LeftTop, ZoomControlPosition.RightTop, ZoomControlPosition.RightCenter, ZoomControlPosition.RightBottom, ZoomControlPosition.BottomCenter, ZoomControlPosition.TopCenter };
        int index;

        public TCMapViewStackLayout3()
        {
            InitializeComponent ();

            var position = new LatLng(Latitude, Longitude);
            _option = new GoogleMapOption(position);
            _option.Zoom = 15;
            _option.MapType = GoogleMapType.Satellite;
            mapview.Update(_option);
            index = 0;
        }

        void OnClickZoomControlVisible(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Zoom invisible")
            {
                btn.Text = "Zoom visible";
                _option.IsZoomControlVisible = true;
            }
            else
            {
                btn.Text = "Zoom invisible";
                _option.IsZoomControlVisible = false;
            }

            mapview.Update(_option);
        }

        void OnClickZoomControlMove(object sender, EventArgs args)
        {
            if (index > 7) index = 0;
            _option.ZoomControlPosition = _positions[index++];
            mapview.Update(_option);
        }

        void OnClickGestureHandle(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Gesture disable")
            {
                btn.Text = "Gesture enable";
                _option.HasGestureEnabled = true;
            }
            else
            {
                btn.Text = "Gesture disable";
                _option.HasGestureEnabled = false;
            }

            mapview.Update(_option);
        }
    }
}