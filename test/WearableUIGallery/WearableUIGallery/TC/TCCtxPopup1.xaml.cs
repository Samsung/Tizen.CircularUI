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
        int[] _xAbsoluteOffsetValue= {0, 0, 0, 100, 100, 100, 200, 200, 200};
        int[] _yAbsoluteOffsetValue = {0, 50, 100, 0, 50, 100, 0, 50, 100};
        int[] _xOffsetValue = { 0, 0, 0, -50, -50, -50, 50, 50, 50};
        int[] _yOffsetValue = { 0, -50, 50,-50, 0, 50, -50, 0, 50};
        double[] _xRelativeOffsetValue = { 0, 0.2, 0.2, 0.5, 0.5, 0.5, 0.7, 0.7, 1.0};
        double[] _yRelativeOffsetValue = { 0, 0.2, 0.5, 0.2, 0.5, 0.7, 0.2, 0.7, 1.0};

        Point OffsetValue = new Point();
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
            if (CtxCheck1EffectBehavior.PositionOption == PositionOption.CenterOfParent) return;

            index++;
            if (index >= 9) index = 0;
            if (CtxCheck1EffectBehavior.PositionOption == PositionOption.BottomOfView)
            {
                OffsetValue.X = _xOffsetValue[index];
                OffsetValue.Y = _yOffsetValue[index];
            }
            else if (CtxCheck1EffectBehavior.PositionOption == PositionOption.Absolute)
            {
                OffsetValue.X = _xAbsoluteOffsetValue[index];
                OffsetValue.Y = _yAbsoluteOffsetValue[index];
            }
            else
            {
                OffsetValue.X = _xRelativeOffsetValue[index];
                OffsetValue.Y = _yRelativeOffsetValue[index];
            }
            CtxCheck1EffectBehavior.Offset = OffsetValue;
            CtxCheck1EffectBehavior.Visibility = true;
        }

        void OnClickPositionOption(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if(CtxCheck1EffectBehavior.PositionOption == PositionOption.Absolute)
            {
                index = 0;
                OffsetValue.X = _xOffsetValue[index];
                OffsetValue.Y = _xOffsetValue[index];
                CtxCheck1EffectBehavior.Offset = OffsetValue;
                CtxCheck1EffectBehavior.PositionOption = PositionOption.BottomOfView;
            }
            else if (CtxCheck1EffectBehavior.PositionOption == PositionOption.BottomOfView)
            {
                OffsetValue.X = 0;
                OffsetValue.Y = 0;
                CtxCheck1EffectBehavior.Offset = OffsetValue;
                CtxCheck1EffectBehavior.PositionOption = PositionOption.CenterOfParent;
            }
            else if (CtxCheck1EffectBehavior.PositionOption == PositionOption.CenterOfParent)
            {
                CtxCheck1EffectBehavior.PositionOption = PositionOption.Relative;
                index = 0;
                OffsetValue.X = _xRelativeOffsetValue[index]; //relative X-postion of Window
                OffsetValue.Y = _yRelativeOffsetValue[index]; //relative Y-postion of Window
                CtxCheck1EffectBehavior.Offset = OffsetValue;
            }
            else
            {
                index = 0;
                CtxCheck1EffectBehavior.PositionOption = PositionOption.Absolute;
                OffsetValue.X = _xAbsoluteOffsetValue[index];
                OffsetValue.Y = _yAbsoluteOffsetValue[index];
                CtxCheck1EffectBehavior.Offset = OffsetValue;
            }

            CtxCheck1EffectBehavior.Visibility = true;
        }

    }
}