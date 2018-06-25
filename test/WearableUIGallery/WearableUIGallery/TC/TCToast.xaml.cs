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
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCToast : ContentPage
    {
        public TCToast()
        {
            InitializeComponent();
        }

        private void OnButton1Clicked(object sender, EventArgs e)
        {
            Toast.DisplayText("Toast popup");
        }

        private void OnButton2Clicked(object sender, EventArgs e)
        {
            Toast.DisplayText("Toast popup with long text and 5 second duration .... qwertyuiopasdfghjklzxcvbnm1234567890", 5000);
        }

        private void OnButton3Clicked(object sender, EventArgs e)
        {
            Toast.DisplayIconText("Icon and text", new FileImageSource { File = "image/tw_ic_popup_btn_check.png" });
        }

        private void OnButton4Clicked(object sender, EventArgs e)
        {
            Toast.DisplayIconText("Big Icon", new FileImageSource { File = "image/tizen.png" }, 2000);
        }

        private void OnButton5Clicked(object sender, EventArgs e)
        {
            Toast.DisplayIconText("Jpg Icon", new FileImageSource { File = "image/a.jpg" }, 2000);
        }
    }
}