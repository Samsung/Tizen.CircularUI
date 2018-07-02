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
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCtxPopup1 : CirclePage
    {
        int[] _xOffsetValue= {0, 0, 0,  100, 100, 100, 200, 200, 200};
        int[] _yOffsetValue= {0, 50, 100, 0, 50, 100, 0, 50, 100};
        public Point OffsetValue = new Point();
        int index;
        public TCCtxPopup1()
        {

            InitializeComponent ();
            index = 0;
            CtxCheck1.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Check.IsToggledProperty.PropertyName)
                    System.Diagnostics.Debug.WriteLine($"IsToggled of CtxCheck1= {CtxCheck1.IsToggled}");
            };
        }

        void OnClickOffset(object sender, EventArgs args)
        {
            if (CtxCheck1EffectBehavior.PositionOption != PositionOption.Absolute) return;

            var btn = sender as Button;
            if (index >= 9) index = 0;
            OffsetValue.X = _xOffsetValue[index];
            OffsetValue.Y = _yOffsetValue[index++];
            CtxCheck1EffectBehavior.Offset = OffsetValue;
            CtxCheck1EffectBehavior.Visibility = true;
        }

        void OnClickPositionOption(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if(CtxCheck1EffectBehavior.PositionOption == PositionOption.Absolute)
            {
                OffsetValue.X = 0;
                OffsetValue.Y = 0;
                CtxCheck1EffectBehavior.Offset = OffsetValue;
                CtxCheck1EffectBehavior.PositionOption = PositionOption.BottomOfView;
            }
            else if (CtxCheck1EffectBehavior.PositionOption == PositionOption.BottomOfView)
            {
                CtxCheck1EffectBehavior.PositionOption = PositionOption.CenterOfParent;
            }
            else if (CtxCheck1EffectBehavior.PositionOption == PositionOption.CenterOfParent)
            {
                CtxCheck1EffectBehavior.PositionOption = PositionOption.Relative;
                OffsetValue.X = 0.5; //relative X-postion of Window
                OffsetValue.Y = 0.3; //relative Y-postion of Window
                CtxCheck1EffectBehavior.Offset = OffsetValue;
            }
            else
            {
                CtxCheck1EffectBehavior.PositionOption = PositionOption.Absolute;
                if (index >= 9) index = 0;
                OffsetValue.X = _xOffsetValue[index];
                OffsetValue.Y = _yOffsetValue[index];
                CtxCheck1EffectBehavior.Offset = OffsetValue;
            }

            CtxCheck1EffectBehavior.Visibility = true;
        }

    }
}