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
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCIndexPage : IndexPage
    {
        int _index;
        IList<ContentPage> _addedContentPages = new List<ContentPage>();

        static Color[] _colors = new Color[] {
            Color.DarkRed,
            Color.IndianRed,
            Color.MediumVioletRed,
            Color.DeepPink,
            Color.HotPink,
            Color.GreenYellow,
            Color.LawnGreen,
            Color.LightGreen,
            Color.LightSeaGreen,
            Color.LimeGreen,
            Color.Navy,
            Color.Orange,
            Color.PaleGreen,
            Color.PaleVioletRed,
            Color.Purple,
            Color.RoyalBlue,
            Color.DeepSkyBlue,
            Color.BlueViolet,
            Color.LightYellow,
            Color.DarkOliveGreen,
            Color.YellowGreen,
        };

        public TCIndexPage ()
        {
            InitializeComponent ();

            _index = 0;
            CurrentPage = GreenPage;
            for (int i = 0; i < 20; i++)
            {
                var page = new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children = {
                        new BoxView {
                            Color = _colors[i],
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand
                        },
                        new Label {
                            AutomationId = $"label{i}",
                            Text = "Added Page(" + i + ")",
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
                    }
                };

                _addedContentPages.Add(page);
            }
        }

        private void OnMoveButtonClicked(object sender, EventArgs e)
        {
            if (_index > 5)
            {
                CurrentPage = _addedContentPages[_index - 1];
            }
            else
            {
                CurrentPage = RedPage;
            }
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (_index > 19) return;
            Children.Add(_addedContentPages[_index++]);

            if (_index > 5)
            {
                MoveButton.Text = "Move to end";
            }
        }
    }
}