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
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCContextPopup : ContentPage
    {
        bool _popupVisibility = false;
        ContextPopup _popup;

        public TCContextPopup()
        {
            InitializeComponent();

            _popup = new ContextPopup();
            var item1 = new ContextPopupItem("item 1");
            var item2 = new ContextPopupItem("item 2");

            _popup.Items.Add(item1);
            _popup.Items.Add(item2);

            _popup.ItemSelected += (s, e) =>
            {
                Console.WriteLine($"{_popup.SelectedItem?.Label} is selected");
                label1.Text = _popup.SelectedItem?.Label + " is selected!";
            };

            _popup.Dismissed += (s, e) =>
            {
                if (_popupVisibility)
                {
                    Console.WriteLine("Popup is dismissed");
                    label1.Text = "Popup is dismissed!";
                    _popupVisibility = false;
                }
            };
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"button1.Clicked _popupVisibility:{_popupVisibility}");
            if (!_popupVisibility)
            {
                _popup.Show(button1, button1.Width / 2, button1.Height);
                _popupVisibility = true;
                label1.Text = "Popup is shown!";
            }
        }
    }
}