﻿/*
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

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleSurfaceItem is a class that controls the items represented in the CircleSurface.
    /// </summary>
    public class CircleSurfaceItem : Element, ICircleSurfaceItem
    {
        /// <summary>
        /// BindableProperty. Identifies the BackgroundAngle bindable property.
        /// </summary>
        public static readonly BindableProperty BackgroundAngleProperty = BindableProperty.Create(nameof(BackgroundAngle), typeof(double), typeof(CircleSurfaceItem), 360.0);

        /// <summary>
        /// BindableProperty. Identifies the BackgroundAngleOffset bindable property.
        /// </summary>
        public static readonly BindableProperty BackgroundAngleOffsetProperty = BindableProperty.Create(nameof(BackgroundAngleOffset), typeof(double), typeof(CircleSurfaceItem), 0.0);

        /// <summary>
        /// BindableProperty. Identifies the BackgroundColor bindable property.
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CircleSurfaceItem), default(Color));

        /// <summary>
        /// BindableProperty. Identifies the BackgroundLineWidth bindable property.
        /// </summary>
        public static readonly BindableProperty BackgroundLineWidthProperty = BindableProperty.Create(nameof(BackgroundLineWidth), typeof(int), typeof(CircleSurfaceItem), -1);

        /// <summary>
        /// BindableProperty. Identifies the BackgroundRadius bindable property.
        /// </summary>
        public static readonly BindableProperty BackgroundRadiusProperty = BindableProperty.Create(nameof(BackgroundRadius), typeof(double), typeof(CircleSurfaceItem), -1.0);

        /// <summary>
        /// BindableProperty. Identifies the BarAngle bindable property.
        /// </summary>
        public static readonly BindableProperty BarAngleProperty = BindableProperty.Create(nameof(BarAngle), typeof(double), typeof(CircleSurfaceItem), 0.0);

        /// <summary>
        /// BindableProperty. Identifies the BarAngleOffset bindable property.
        /// </summary>
        public static readonly BindableProperty BarAngleOffsetProperty = BindableProperty.Create(nameof(BarAngleOffset), typeof(double), typeof(CircleSurfaceItem), 0.0);

        /// <summary>
        /// BindableProperty. Identifies the BarAngleMaximum bindable property.
        /// </summary>
        public static readonly BindableProperty BarAngleMaximumProperty = BindableProperty.Create(nameof(BarAngleMaximum), typeof(double), typeof(CircleSurfaceItem), 360.0);

        /// <summary>
        /// BindableProperty. Identifies the BarAngleMinimum bindable property.
        /// </summary>
        public static readonly BindableProperty BarAngleMinimumProperty = BindableProperty.Create(nameof(BarAngleMinimum), typeof(double), typeof(CircleSurfaceItem), 0.0);

        /// <summary>
        /// BindableProperty. Identifies the BarColor bindable property.
        /// </summary>
        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(nameof(BarColor), typeof(Color), typeof(CircleSurfaceItem), default(Color));

        /// <summary>
        /// BindableProperty. Identifies the BarLineWidth bindable property.
        /// </summary>
        public static readonly BindableProperty BarLineWidthProperty = BindableProperty.Create(nameof(BarLineWidth), typeof(int), typeof(CircleSurfaceItem), -1);

        /// <summary>
        /// BindableProperty. Identifies the BarRadius bindable property.
        /// </summary>
        public static readonly BindableProperty BarRadiusProperty = BindableProperty.Create(nameof(BarRadius), typeof(double), typeof(CircleSurfaceItem), -1.0);

        /// <summary>
        /// BindableProperty. Identifies the IsVisible bindable property.
        /// </summary>
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CircleSurfaceItem), true);

        /// <summary>
        /// BindableProperty. Identifies the IsEnabled bindable property.
        /// </summary>
        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CircleSurfaceItem), true);

        /// <summary>
        /// Gets or sets the background angle value.
        /// If background angle is 180, background of surface item draw 180 degree from background angle offset.
        /// </summary>
        public double BackgroundAngle { get => (double)GetValue(BackgroundAngleProperty); set => SetValue(BackgroundAngleProperty, value); }

        /// <summary>
        /// Gets or sets the background angle offset value.
        /// if background angle offset is 30, background of surface item start at 30 degree.
        /// </summary>
        public double BackgroundAngleOffset { get => (double)GetValue(BackgroundAngleOffsetProperty); set => SetValue(BackgroundAngleOffsetProperty, value); }

        /// <summary>
        /// Gets or sets the background color value.
        /// </summary>
        public Color BackgroundColor { get => (Color)GetValue(BackgroundColorProperty); set => SetValue(BackgroundColorProperty, value); }

        /// <summary>
        /// Gets or sets the background line width value.
        /// </summary>
        public int BackgroundLineWidth { get => (int)GetValue(BackgroundLineWidthProperty); set => SetValue(BackgroundLineWidthProperty, value); }

        /// <summary>
        /// Gets or sets the background radius value.
        /// </summary>
        public double BackgroundRadius { get => (double)GetValue(BackgroundRadiusProperty); set => SetValue(BackgroundRadiusProperty, value); }

        /// <summary>
        /// Gets or sets the bar angle value.
        /// If bar angle is 180, bar of surface item draw 180 degree from bar angle offset.
        /// </summary>
        public double BarAngle { get => (double)GetValue(BarAngleProperty); set => SetValue(BarAngleProperty, value); }

        /// <summary>
        /// Gets or sets the bar angle offset value.
        /// if bar angle offset is 30, bar of surface item start at 30 degree.
        /// </summary>
        public double BarAngleOffset { get => (double)GetValue(BarAngleOffsetProperty); set => SetValue(BarAngleOffsetProperty, value); }

        /// <summary>
        /// Gets or sets the bar angle maximum value.
        /// </summary>
        public double BarAngleMaximum { get => (double)GetValue(BarAngleMaximumProperty); set => SetValue(BarAngleMaximumProperty, value); }

        /// <summary>
        /// Gets or sets the bar angle minimum value.
        /// </summary>
        public double BarAngleMinimum { get => (double)GetValue(BarAngleMinimumProperty); set => SetValue(BarAngleMinimumProperty, value); }

        /// <summary>
        /// Gets or sets the bar color value.
        /// </summary>
        public Color BarColor { get => (Color)GetValue(BarColorProperty); set => SetValue(BarColorProperty, value); }

        /// <summary>
        /// Gets or sets the bar line width value.
        /// </summary>
        public int BarLineWidth { get => (int)GetValue(BarLineWidthProperty); set => SetValue(BarLineWidthProperty, value); }

        /// <summary>
        /// Gets or sets the bar radius value.
        /// </summary>
        public double BarRadius { get => (double)GetValue(BarRadiusProperty); set => SetValue(BarRadiusProperty, value); }

        /// <summary>
        /// Gets or sets the visibility value of circle surface item.
        /// </summary>
        public bool IsVisible { get => (bool)GetValue(IsVisibleProperty); set => SetValue(IsVisibleProperty, value); }

        /// <summary>
        /// Gets or sets a value indicating whether this element is enabled.
        /// </summary>
        public bool IsEnabled { get => (bool)GetValue(IsEnabledProperty); set => SetValue(IsEnabledProperty, value); }
    }
}
