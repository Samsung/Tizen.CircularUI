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
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCCircleStepper2 : CirclePage
    {
        public TCCircleStepper2()
        {
            InitializeComponent ();
        }

        void OnFocusedT1(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperT1;
        }
        void OnFocusedT2(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperT2;
        }
        void OnFocusedT3(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperT3;
            StepperT3.LabelFormat="%1.1f";
        }
    }
}