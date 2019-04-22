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
    public partial class TCActionButton : CirclePage
    {
        public TCActionButton ()
        {
            InitializeComponent ();
        }

        void OnClickEnable(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if (btn.Text == "Enable")
            {
                btn.Text = "Disable";
                ActionButton.SetValue(ActionButtonItem.IsEnableProperty, false);
            }
            else
            {
                btn.Text = "Enable";
                ActionButton.IsEnable = true;
            }
        }

        void OnClickVisible(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if (btn.Text == "Invisible")
            {
                btn.Text = "Visible";
                ActionButton.SetValue(ActionButtonItem.IsVisibleProperty, true);
            }
            else
            {
                btn.Text = "Invisible";
                ActionButton.IsVisible = false;
            }
        }


        void OnClickSet(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Set")
            {
                btn.Text = "Unset";
                ActionButton = new ActionButtonItem { Text = "Action" };
            }
            else
            {
                btn.Text = "Set";
                ActionButton = null;
            }
        }

        void OnClickChangeColor(object sender, EventArgs args)
        {
            ActionButton = new ActionButtonItem { Text = "Action", BackgroundColor = Color.Green };
        }
    }
}