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
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCPopupEntry : BezelInteractionPage
    {
        static string Validname = "hostname";

        public TCPopupEntry()
        {
            InitializeComponent();
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            this.PopupEntry2.IsPopupOpened = !this.PopupEntry2.IsPopupOpened;
            Console.WriteLine("ButtonClicked = " + this.PopupEntry1.IsPopupOpened);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (Validname.Equals(hostname_entry.Text))
            {
                hostname_entry.TextColor = Color.White;
                hostname_entry.PopupBackgroundColor = Color.DarkCyan;
                hostname_entry.FontAttributes = FontAttributes.Bold;
            }
            else
            {
                hostname_entry.TextColor = Color.Red;
                hostname_entry.PopupBackgroundColor = Color.Green;
                hostname_entry.FontAttributes = FontAttributes.Italic;
            }

            hostname_entry.BackgroundColor = hostname_entry.PopupBackgroundColor;
        }
    }
}