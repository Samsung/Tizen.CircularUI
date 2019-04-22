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
    public partial class TCIndexPageFocus : IndexPage
    {
        public TCIndexPageFocus()
        {
            InitializeComponent();
        }

        void OnFocused1(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused1");
            firstPage.RotaryFocusObject = stepper1;
        }

        void OnFocused2(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused2");
            firstPage.RotaryFocusObject = stepper2;
        }

        void OnFocused3(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused3");
            thirdPage.RotaryFocusObject = stepper3;
        }

        void OnFocused4(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused4");
            thirdPage.RotaryFocusObject = stepper4;
        }

        void OnEntryFocused1(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnEntryFocused1");
        }

        void OnEntryFocused2(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnEntryFocused2");
        }
    }
}