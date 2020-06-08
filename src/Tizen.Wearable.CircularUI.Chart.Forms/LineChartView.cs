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

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// The LineChartView is a chart that shows information as a series of data points connected by line. 
    /// </summary>
    public class LineChartView : ChartView
    {
        /// <summary>
        /// BindableProperty. Identifies the LineMode bindable property.
        /// </summary>
        public static readonly BindableProperty LineModeProperty = BindableProperty.Create(nameof(LineMode), typeof(LineMode), typeof(LineChartView), LineMode.Straight);

        /// <summary>
        /// BindableProperty. Identifies the LineWidth bindable property.
        /// </summary>
        public static readonly BindableProperty LineWidthProperty = BindableProperty.Create(nameof(LineWidth), typeof(double), typeof(LineChartView), 2d);

        /// <summary>
        /// BindableProperty. Identifies the AxisOption bindable property.
        /// </summary>
        public static readonly BindableProperty AxisOptionProperty = BindableProperty.Create(nameof(AxisOption), typeof(AxisOption), typeof(LineChartView), default(AxisOption));

        /// <summary>
        /// BindableProperty. Identifies the PointIsVisible bindable property.
        /// </summary>
        public static readonly BindableProperty PointIsVisibleProperty = BindableProperty.Create(nameof(PointIsVisible), typeof(bool), typeof(LineChartView), true);

        /// <summary>
        /// BindableProperty. Identifies the PointRadius bindable property.
        /// </summary>
        public static readonly BindableProperty PointRadiusProperty = BindableProperty.Create(nameof(PointRadius), typeof(double), typeof(LineChartView), 4d);

        /// <summary>
        /// Gets or sets type of line.
        /// </summary>
        public LineMode LineMode
        {
            get { return (LineMode)GetValue(LineModeProperty); }
            set { SetValue(LineModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets width of line.
        /// </summary>
        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the AxisOption.
        /// </summary>
        public AxisOption AxisOption
        {
            get { return (AxisOption)GetValue(AxisOptionProperty); }
            set { SetValue(AxisOptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of points.
        /// </summary>
        public bool PointIsVisible
        {
            get { return (bool)GetValue(PointIsVisibleProperty); }
            set { SetValue(PointIsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of points.
        /// </summary>
        public double PointRadius
        {
            get { return (double)GetValue(PointRadiusProperty); }
            set { SetValue(PointRadiusProperty, value); }
        }
    }
}
