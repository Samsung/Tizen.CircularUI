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
        MenuItem _textBottomButton;
        MenuItem _iconBottomButton;
        MenuItem _textIconBottomButton;
        string _longText;

        public TCInformationPopup()
        {
            InitializeComponent();

            _textBottomButton = new MenuItem
            {
                Text = "OK",
                Command = new Command(() =>
                {
                    Console.WriteLine("text bottom button Command!!");
                    _textButtonPopUp?.Dismiss();
                    _textButtonPopUp = null;
                })
            };

            _iconBottomButton = new MenuItem
            {
                Icon = new FileImageSource
                {
                    File = "image/tw_ic_popup_btn_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("icon bottom button Command!!");
                    _textButtonPopUp?.Dismiss();
                    _textButtonPopUp = null;
                })
            };

            _textIconBottomButton = new MenuItem
            {
                Text = "OK",
                Icon = new FileImageSource
                {
                    File = "image/tw_ic_popup_btn_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("text&icon bottom button Command!!");
                    _textButtonPopUp?.Dismiss();
                    _textButtonPopUp = null;
                })
            };

            _longText = @"This is scrollable popup text.
This part is made by adding long text in popup.Popup internally added
scroller to this layout when size of text is greater than total popup
height.This has two button in action area and title text in title area";
        }

        void createTextPopup()
        {
            _textPopUp = new InformationPopup();

            _textPopUp.BackButtonPressed += (s, e) =>
            {
                _textPopUp?.Dismiss();
                _textPopUp = null;
                label1.Text = "text popup is dismissed";
            };

        }

        void createTextButtonPopup()
        {
            _textButtonPopUp = new InformationPopup();
            _textButtonPopUp.Title = "Popup title";
            _textButtonPopUp.Text = "This is text and button popup test";
            _textButtonPopUp.BottomButton = _textBottomButton;

            _textButtonPopUp.BackButtonPressed += (s, e) =>
            {
                _textButtonPopUp?.Dismiss();
                _textButtonPopUp = null;
                label1.Text = "text&button is dismissed";
            };

            _textButtonPopUp.BottomButton.Clicked += (s, e) =>
            {
                _textButtonPopUp?.Dismiss();
                _textButtonPopUp = null;
                label1.Text = "text&button is dismissed";
            };
        }

        void createProgressPopup()
        {
            _progressPopUp = new InformationPopup();
            _progressPopUp.Title = "Popup title";
            _progressPopUp.Text = "This is progress test";
            _progressPopUp.IsProgressRunning = true;

            _progressPopUp.BackButtonPressed += (s, e) =>
            {
                _progressPopUp?.Dismiss();
                _progressPopUp = null;
                label1.Text = "progress is dismissed";
            };
        }

        private void OnTextButtonClicked(object sender, EventArgs e)
        {
            createTextPopup();
            _textPopUp.Text = "This is text popup test";

            _textPopUp.Show();
        }

        private void OnLongTextButtonClicked(object sender, EventArgs e)
        {
            createTextPopup();
            _textPopUp.Text = _longText;
            _textPopUp.Show();
        }

        private void OnTitleTextButtonClicked(object sender, EventArgs e)
        {
            createTextButtonPopup();
            _textButtonPopUp.Text = "This is text and button popup test";
            _textButtonPopUp.BottomButton = _textBottomButton;
            _textButtonPopUp.Show();
        }
        private void OnIconBottomButtonClicked(object sender, EventArgs e)
        {
            createTextButtonPopup();
            _textButtonPopUp.Text = _longText;
            _textButtonPopUp.BottomButton = _iconBottomButton;
            _textButtonPopUp.Show();
        }

        private void OnIconAndTextBottomButtonClicked(object sender, EventArgs e)
        {
            createTextButtonPopup();
            _textButtonPopUp.Text = "This is text and button popup test";
            _textButtonPopUp.BottomButton = _textIconBottomButton;
            _textButtonPopUp.Show();
        }

        private void OnProcessButtonClicked(object sender, EventArgs e)
        {
            createProgressPopup();
            _progressPopUp.Show();

            Device.StartTimer(TimeSpan.FromMilliseconds(3000), () =>
            {
                _progressPopUp?.SetValue(InformationPopup.TitleProperty, "Stopped Popup");
                _progressPopUp?.SetValue(InformationPopup.TextProperty, "Progress is finished");
                _progressPopUp?.SetValue(InformationPopup.IsProgressRunningProperty, false);
                _progressPopUp?.SetValue(InformationPopup.BottomButtonProperty, new MenuItem
                {
                    Text = "Reset",
                    Command = new Command(() => {
                        _progressPopUp.Title = "Popup title";
                        _progressPopUp.Text = "This is progress test";
                        _progressPopUp.IsProgressRunning = true;
                        _progressPopUp.BottomButton = null;
                    })
                });
                return false;
            });
        }

        private void OnProcessLongTextButtonClicked(object sender, EventArgs e)
        {
            createProgressPopup();
            _progressPopUp.Text = @"This is scrollable popup text.
This part is made by adding long text in popup.Popup internally added
scroller to this layout when size of text is greater than total popup
height.This has two button in action area and title text in title area";
            _progressPopUp.Show();
        }

    }
}