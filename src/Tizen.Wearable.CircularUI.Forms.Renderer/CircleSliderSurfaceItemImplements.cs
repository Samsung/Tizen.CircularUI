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
using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleSliderSurfaceItemImplements : ElmSharp.Wearable.CircleSlider
    {
        CircleSliderSurfaceItem _item;

        public CircleSliderSurfaceItemImplements(CircleSliderSurfaceItem item, EvasObject parent, CircleSurface surface) : base(parent, surface)
        {
            _item = item;
            item.PropertyChanged += ItemPropertyChanged;
            ValueChanged += OnValueChanged;

            BackgroundAngle = item.BackgroundAngle;
            BackgroundAngleOffset = item.BackgroundAngleOffset;
            if (item.BackgroundColor != default(Xamarin.Forms.Color)) BackgroundColor = item.BackgroundColor.ToNative();
            if (item.BackgroundLineWidth != -1) BackgroundLineWidth = item.BackgroundLineWidth;
            if (item.BackgroundRadius != -1) BackgroundRadius = item.BackgroundRadius;

            BarAngleOffset = item.BarAngleOffset;
            BarAngleMaximum = item.BarAngleMaximum;
            BarAngleMinimum = item.BarAngleMinimum;
            if (item.BarColor != default(Xamarin.Forms.Color)) BarColor = item.BarColor.ToNative();
            if (item.BarLineWidth != -1) BarLineWidth = item.BarLineWidth;
            if (item.BarRadius != -1) BarRadius = item.BarRadius;

            Minimum = item.Minimum;
            Maximum = item.Maximum;
            Step = item.Increment;

            if (item.Value == item.Minimum && item.BarAngle != 0)
            {
                BarAngle = item.BarAngle;
            }
            else
            {
                Value = item.Value;
            }

            IsEnabled = item.IsEnabled;

            if (item.IsVisible) Show();
            else Hide();
        }

        void ItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == CircleSliderSurfaceItem.BackgroundAngleProperty.PropertyName)
            {
                BackgroundAngle = _item.BackgroundAngle;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundAngleOffsetProperty.PropertyName)
            {
                BackgroundAngleOffset = _item.BackgroundAngleOffset;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundColorProperty.PropertyName)
            {
                BackgroundColor = _item.BackgroundColor.ToNative();
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundLineWidthProperty.PropertyName)
            {
                BackgroundLineWidth = _item.BackgroundLineWidth;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundRadiusProperty.PropertyName)
            {
                BackgroundRadius = _item.BackgroundRadius;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleProperty.PropertyName)
            {
                BarAngle = _item.BarAngle;
                if (_item.Value != Value)
                {
                    _item.Value = Value;
                }
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleOffsetProperty.PropertyName)
            {
                BarAngleOffset = _item.BarAngleOffset;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleMaximumProperty.PropertyName)
            {
                BarAngleMaximum = _item.BarAngleMaximum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleMinimumProperty.PropertyName)
            {
                BarAngleMinimum = _item.BarAngleMinimum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarColorProperty.PropertyName)
            {
                BarColor = _item.BarColor.ToNative();
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarLineWidthProperty.PropertyName)
            {
                BarLineWidth = _item.BarLineWidth;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarRadiusProperty.PropertyName)
            {
                BarRadius = _item.BarRadius;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.IsVisibleProperty.PropertyName)
            {
                if (_item.IsVisible) Show();
                else Hide();
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.ValueProperty.PropertyName)
            {
                if (_item.Value != Value) Value = _item.Value;
                _item.BarAngle = BarAngle;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.IsEnabledProperty.PropertyName)
            {
                IsEnabled = _item.IsEnabled;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.MinimumProperty.PropertyName)
            {
                Minimum = _item.Minimum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.MaximumProperty.PropertyName)
            {
                Maximum = _item.Maximum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.IncrementProperty.PropertyName)
            {
                Step = _item.Increment;
            }
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            if (_item.Value != Value) _item.Value = Value;
        }
    }
}
