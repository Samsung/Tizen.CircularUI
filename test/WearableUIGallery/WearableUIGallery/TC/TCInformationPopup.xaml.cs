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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCInformationPopup : ContentPage
    {
        InformationPopup _textPopUp = null;
        InformationPopup _textButtonPopUp = null;
        InformationPopup _progressPopUp = null;

        public TCInformationPopup()
        {
            InitializeComponent();

            _textPopUp = new InformationPopup();
            _textPopUp.Text = "This is text popup test";

            _textPopUp.BackButtonPressed += (s, e) =>
            {
                _textPopUp.Dismiss();
                label1.Text = "text popup is dismissed";
            };

            var bottomButton = new MenuItem()
            {
                Text = "OK",
                Command = new Command(() =>
                {
                    Console.WriteLine("bottom button Command!!");
                    _textButtonPopUp.Dismiss();
                })
            };

            _textButtonPopUp = new InformationPopup();
            _textButtonPopUp.BottomButton = bottomButton;
            _textButtonPopUp.Title = "Popup title";
            _textButtonPopUp.Text = "This is text and button popup test";
            _textButtonPopUp.BottomButton = bottomButton;

            _textButtonPopUp.BackButtonPressed += (s, e) =>
            {
                _textButtonPopUp.Dismiss();
                label1.Text = "text&button is dismissed";
            };

            _textButtonPopUp.BottomButton.Clicked += (s, e) =>
            {
                _textButtonPopUp.Dismiss();
                label1.Text = "text&button is dismissed";
            };

            _progressPopUp = new InformationPopup();
            _progressPopUp.Title = "Popup title";
            _progressPopUp.Text = "This is progress test";
            _progressPopUp.IsProgressRunning = true;

            _progressPopUp.BackButtonPressed += (s, e) =>
            {
                _progressPopUp.Dismiss();
                label1.Text = "progress is dismissed";
            };

        }


        private void OnButton1Clicked(object sender, EventArgs e)
        {
            _textPopUp.Show();
        }

        private void OnButton2Clicked(object sender, EventArgs e)
        {
            _textButtonPopUp.Show();
        }

        private void OnButton3Clicked(object sender, EventArgs e)
        {
            _progressPopUp.Show();
        }
    }
}