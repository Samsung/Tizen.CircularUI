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
    public partial class TCRadioStackLayout : ContentPage
    {
        public TCRadioStackLayout()
        {
            InitializeComponent();
        }

        public void OnSelected(object sender, SelectedEventArgs args)
        {
            Radio radio = sender as Radio;
            if (radio != null)
            {
                Console.WriteLine($"<<OnSelected>>  args.Value:{args.Value}, args.IsSelected:{args.IsSelected}");
                Console.WriteLine($"<<OnSelected>>  Radio Value:{radio.Value}, IsSelected:{radio.IsSelected}, GroupName:{radio.GroupName}");
            }
        }
    }
}