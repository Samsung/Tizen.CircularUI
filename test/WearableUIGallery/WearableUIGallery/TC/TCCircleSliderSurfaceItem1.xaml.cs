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
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCircleSliderSurfaceItem1 : CirclePage
    {

        public TCCircleSliderSurfaceItem1()
        {
            InitializeComponent ();
        }

        void OnClick(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text.Equals("Enable"))
            {
                slider.Value = 12;
                slider.IsEnabled = true;
                btn.Text = "Disable";
            }
            else
            {
                slider.BarAngle = 60;
                slider.IsEnabled = false;
                btn.Text = "Enable";
            }
        }
    }
}