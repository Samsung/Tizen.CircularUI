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
    public partial class TCTwoButtonPopupCmd : ContentPage
    {
        TitleText2Button popup;

        public TCTwoButtonPopupCmd()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            popup = new TitleText2Button("Title Text", "Hello Xamarin.Forms!! ");

            popup.ButtonClicked += (o, i) =>
            {
                var result = (bool)o;
                Console.WriteLine($"ButtonClicked event Invoked! result:{result}");
                if (result)
                {
                    Toast.DisplayText("Set Button Clicked!");
                }
                else
                {
                    Toast.DisplayText("Cancel Button Clicked!");
                }
                popup.Dismiss();
            };
            popup.Show();
        }

    }


    public class TitleText2Button : TwoButtonPopup
    {

        public event EventHandler ButtonClicked;
        /// <summary>
        /// Constructor of TitleText2Button class
        /// </summary>
        public TitleText2Button(string title, string text)
        {

            Title = title;
            Text = text;
            // Create FirstButton
            FirstButton = new MenuItem()
            {
                // Set icon
                IconImageSource = ImageSource.FromFile("image/tw_ic_popup_btn_delete.png"),
                //Set command
                Command = new Command(() =>
                {
                    ButtonClicked.Invoke(false, new EventArgs());
                })
            };

            // Create SecondButton
            SecondButton = new MenuItem()
            {
                // Set icon
                IconImageSource = ImageSource.FromFile("image/tw_ic_popup_btn_check.png"),
                //Set command
                Command = new Command(() =>
                {
                    ButtonClicked.Invoke(true, new EventArgs());
                })
            };

            //Request to dismiss this popup when back button event occurs
            BackButtonPressed += (s, e) => {
                ButtonClicked.Invoke(false, new EventArgs());
            };
        }
    }
}