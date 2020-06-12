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
using System.Collections.Generic;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCircleToolbarItem : ContentPage
    {
        public TCCircleToolbarItem()
        {
            InitializeComponent();
            cntLabel.Text = "# of items :"+ ToolbarItems.Count;
        }

        void OnItemAddClicked(object sender, System.EventArgs e)
        {
            AddToolbarItem("ToolbarItem", "ID :" + ToolbarItems.Count, "image/icon_alert_sound.png");
        }

        void OnItemRemoveClicked(object sender, System.EventArgs e)
        {
            RemoveToolbarItem();
        }

        void AddToolbarItem(string mainText, string subText, string icon)
        {
            CircleToolbarItem item = new CircleToolbarItem
            {
                Text = mainText,
                SubText = subText,
                IconImageSource = icon
            };
            ToolbarItems.Add(item);
            cntLabel.Text = "# of items :" + ToolbarItems.Count;
        }

        void RemoveToolbarItem()
        {
            var id = ToolbarItems.Count - 1;
            if (id < 0)
                return;
            ToolbarItems.RemoveAt(id);
            cntLabel.Text = "# of items :" + ToolbarItems.Count;
        }
    }
}