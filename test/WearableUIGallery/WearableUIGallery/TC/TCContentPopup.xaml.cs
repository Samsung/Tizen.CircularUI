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
    public partial class TCContentPopupTest : ContentPage
    {
        public TCContentPopupTest()
        {
            InitializeComponent();
        }

        private void OnContentPopupDismissBackKeyClicked(object sender, EventArgs e)
        {
            ContentPopup _popup = new ContentPopup();
            _popup.BackButtonPressed += (s, ee) =>
            {
                _popup?.Dismiss();
                label1.Text = "Test1 Dismissed";
            };

            string _longText = "This ContentPopup is dismissed as a back key.";
            var content = new Label { Text = _longText, HorizontalTextAlignment = TextAlignment.Center };

            _popup.Content = content;
            _popup.Show();
        }

        private void OnContentPopupDismissButtonClicked(object sender, EventArgs e)
        {
            ContentPopup _popup = new ContentPopup();
            var dismiss = new Button
            {
                Text = "Dismiss",
            };
            dismiss.Clicked += (s, ee) => {
                _popup?.Dismiss();
                label1.Text = "Test2 Dismissed";
            };

            var label = new Label
            {
                Text = "This ContentPopup is dismissed as a below dismiss button.",
                HorizontalTextAlignment = TextAlignment.Center,
            };

            var grid = new Grid();
            grid.HeightRequest = 360;
            grid.WidthRequest = 360;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.Children.Add(label, 0, 1, 1, 3);
            grid.Children.Add(dismiss, 0, 1, 3, 4);

            _popup.Content = grid;
            _popup.Show();
        }
    }
}