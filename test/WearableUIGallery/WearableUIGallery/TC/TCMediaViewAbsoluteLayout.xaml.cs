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
    public partial class TCMediaViewAbsoluteLayout : CirclePage
    {
        double _x, _y;

        public TCMediaViewAbsoluteLayout()
        {
            InitializeComponent ();
        }

        void OnPanUpdate(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && e.TotalY != 0)
            {
                
                AbsoluteLayout.SetLayoutBounds(VideoView, new Rectangle(_x + e.TotalX, _y + e.TotalY, VideoView.Width, VideoView.Height));
            }
            else
            {
                var bound = AbsoluteLayout.GetLayoutBounds(VideoView);
                _x = bound.X;
                _y = bound.Y;
            }
        }
    }
}