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

using ElmSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;

using Xamarin.Forms.Platform.Tizen;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    class CircleProgressBarSurfaceItemImplements : ElmSharp.Wearable.CircleProgressBar
    {
        CircleProgressBarSurfaceItem _item;

        public CircleProgressBarSurfaceItemImplements(CircleProgressBarSurfaceItem item, EvasObject parent, ElmSharp.Wearable.CircleSurface surface) : base(parent, surface)
        {
            _item = item;
            item.PropertyChanged += ItemPropertyChanged;

            BackgroundAngle = item.BackgroundAngle;
            BackgroundAngleOffset = item.BackgroundAngleOffset;
            if (item.BackgroundColor != default(Xamarin.Forms.Color)) BackgroundColor = item.BackgroundColor.ToNative();
            if (item.BackgroundLineWidth != -1) BackgroundLineWidth = item.BackgroundLineWidth;
            if (item.BackgroundRadius != -1) BackgroundRadius = item.BackgroundRadius;

            BarAngle = item.BarAngle;
            BarAngleOffset = item.BarAngleOffset;
            BarAngleMaximum = item.BarAngleMaximum;
            BarAngleMinimum = item.BarAngleMinimum;
            if (item.BarColor != default(Xamarin.Forms.Color)) BarColor = item.BarColor.ToNative();
            if (item.BarLineWidth != -1) BarLineWidth = item.BarLineWidth;
            if (item.BarRadius != -1) BarRadius = item.BarRadius;

            Minimum = 0;
            Maximum = 1;

            Value = item.Value;
            IsEnabled = item.IsEnabled;

            if (item.IsVisible) Show();
            else Hide();
        }

        void ItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == CircleProgressBarSurfaceItem.BackgroundAngleProperty.PropertyName)
            {
                BackgroundAngle = _item.BackgroundAngle;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BackgroundAngleOffsetProperty.PropertyName)
            {
                BackgroundAngleOffset = _item.BackgroundAngleOffset;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BackgroundColorProperty.PropertyName)
            {
                BackgroundColor = _item.BackgroundColor.ToNative();
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BackgroundLineWidthProperty.PropertyName)
            {
                BackgroundLineWidth = _item.BackgroundLineWidth;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BackgroundRadiusProperty.PropertyName)
            {
                BackgroundRadius = _item.BackgroundRadius;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarAngleProperty.PropertyName)
            {
                BarAngle = _item.BarAngle;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarAngleOffsetProperty.PropertyName)
            {
                BarAngleOffset = _item.BarAngleOffset;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarAngleMaximumProperty.PropertyName)
            {
                BarAngleMaximum = _item.BarAngleMaximum;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarAngleMinimumProperty.PropertyName)
            {
                BarAngleMinimum = _item.BarAngleMinimum;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarColorProperty.PropertyName)
            {
                BarColor = _item.BarColor.ToNative();
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarLineWidthProperty.PropertyName)
            {
                BarLineWidth = _item.BarLineWidth;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.BarRadiusProperty.PropertyName)
            {
                BarRadius = _item.BarRadius;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.IsVisibleProperty.PropertyName)
            {
                if (_item.IsVisible) Show();
                else Hide();
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.ValueProperty.PropertyName)
            {
                Value = _item.Value;
            }
            else if (args.PropertyName == CircleProgressBarSurfaceItem.IsEnabledProperty.PropertyName)
            {
                IsEnabled = _item.IsEnabled;
            }
        }
    }
}
