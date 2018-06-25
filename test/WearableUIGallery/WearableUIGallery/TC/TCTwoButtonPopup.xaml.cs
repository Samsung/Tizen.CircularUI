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
    public partial class TCTwoButtonPopup : ContentPage
    {
        TwoButtonPopup _popUp1 = null;
        TwoButtonPopup _popUp2 = null;
        MenuItem _leftButton;
        MenuItem _rightButton;
        MenuItem _noIconLeftButton;
        MenuItem _noIconRightButton;
        MenuItem _jpgIconButton1;
        MenuItem _jpgIconButton2;

        public TCTwoButtonPopup()
        {
            InitializeComponent();

            _leftButton = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "image/b_option_list_icon_share.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("left button1 Command!!");
                    _popUp1.Dismiss();
                })
            };

            _rightButton = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "image/b_option_list_icon_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                    _popUp1.Dismiss();
                })
            };

            _noIconLeftButton = new MenuItem()
            {
                Text = "No icon left button",
                Command = new Command(() =>
                {
                    Console.WriteLine("No icon left button Command!!");
                    _popUp1.Dismiss();
                })
            };

            _noIconRightButton = new MenuItem()
            {
                Text = "No icon right button",
                Command = new Command(() =>
                {
                    Console.WriteLine("no icon right button Command!!");
                    _popUp1.Dismiss();
                })
            };

            _jpgIconButton1 = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "image/a.jpg",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("jpg button1 Command!!");
                    _popUp1.Dismiss();
                })
            };

            _jpgIconButton2 = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "image/b.jpg",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("jpg button2 Command!!");
                    _popUp1.Dismiss();
                })
            };


            var checkbox = new Check
            {
                DisplayStyle = CheckDisplayStyle.Small
            };

            checkbox.Toggled += (s, e) =>
            {
                Console.WriteLine($"checkbox toggled. checkbox.IsToggled:{checkbox.IsToggled}");
            };

            _popUp1 = new TwoButtonPopup();
            _popUp1.Title = "Popup title";
            _popUp1.FirstButton = _leftButton;
            _popUp1.SecondButton = _rightButton;
            _popUp1.Content = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "Will be saved, and sound, only on the Gear.",
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 40, 0, 40),
                        Children =
                        {
                            checkbox,
                            new Label
                            {
                                Text = "Do not repeat",
                            }
                        }
                    }
                }
            };

            _popUp1.BackButtonPressed += (s, e) =>
            {
                _popUp1.Dismiss();
                label1.Text = "Popup1 is dismissed";
            };

            var leftButton2 = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "image/tw_ic_popup_btn_check.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("left button2 Command!!");
                    _popUp2.Dismiss();
                })
            };

            var rightButton2 = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "image/tw_ic_popup_btn_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("right button2 Command!!");
                    _popUp2.Dismiss();
                })
            };

            _popUp2 = new TwoButtonPopup();
            _popUp2.FirstButton = leftButton2;
            _popUp2.SecondButton = rightButton2;
            _popUp2.Title = "Popup title";
            _popUp2.Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has two button in action area and title text in title area";

            _popUp2.BackButtonPressed += (s, e) =>
            {
                _popUp2.Dismiss();
                label1.Text = "Popup2 is dismissed";
            };

            _popUp1.FirstButton.Clicked += (s, e) => Console.WriteLine("First(share) button clicked!");
            _popUp1.SecondButton.Clicked += (s, e) => Console.WriteLine("Second(delete) button clicked!");

        }


        private void OnTwoButtonTextClicked(object sender, EventArgs e)
        {
            _popUp1.FirstButton = _leftButton;
            _popUp1.SecondButton = _rightButton;
            _popUp1.Show();
        }

        private void OnTwoButtonLongTextClicked(object sender, EventArgs e)
        {
            _popUp2.Show();
        }

        private void OnLeftOnlyClicked(object sender, EventArgs e)
        {
            _popUp1.FirstButton = _leftButton;
            _popUp1.SecondButton = null;
            _popUp1.Show();
        }

        private void OnRightOnlyClicked(object sender, EventArgs e)
        {
            _popUp1.FirstButton = null;
            _popUp1.SecondButton = _rightButton;
            _popUp1.Show();
        }

        private void OnLeftNoIconClicked(object sender, EventArgs e)
        {
            _popUp1.FirstButton = _noIconLeftButton;
            _popUp1.SecondButton = _rightButton;
            _popUp1.Show();
        }

        private void OnRightNoIconClicked(object sender, EventArgs e)
        {
            _popUp1.FirstButton = _leftButton;
            _popUp1.SecondButton = _noIconRightButton;
            _popUp1.Show();
        }

        private void OnRightJpgIconClicked(object sender, EventArgs e)
        {
            _popUp1.FirstButton = _jpgIconButton1;
            _popUp1.SecondButton = _jpgIconButton2;
            _popUp1.Show();
        }
    }
}