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
    /// The BarChartView is a view that represents vertical/horizontal bar chart.
    /// </summary>
    public class BarChartView : ChartView
    {
        /// <summary>
        /// BindableProperty. Identifies the BarChartType bindable property.
        /// </summary>
        public static readonly BindableProperty BarChartTypeProperty = BindableProperty.Create(nameof(BarChartType), typeof(BarChartType), typeof(BarChartView), BarChartType.Vertical);

        /// <summary>
        /// BindableProperty. Identifies the BarWidth bindable property.
        /// </summary>
        public static readonly BindableProperty BarWidthProperty = BindableProperty.Create(nameof(BarWidth), typeof(double), typeof(BarChartView), 10d);

        /// <summary>
        /// BindableProperty. Identifies the BarTopRadius bindable property.
        /// </summary>
        public static readonly BindableProperty BarTopRadiusProperty = BindableProperty.Create(nameof(BarTopRadius), typeof(double), typeof(BarChartView), 0d);

        /// <summary>
        /// BindableProperty. Identifies the BarBottomRadius bindable property.
        /// </summary>
        public static readonly BindableProperty BarBottomRadiusProperty = BindableProperty.Create(nameof(BarBottomRadius), typeof(double), typeof(BarChartView), 0d);

        /// <summary>
        /// BindableProperty. Identifies the AxisOption bindable property.
        /// </summary>
        public static readonly BindableProperty AxisOptionProperty = BindableProperty.Create(nameof(AxisOption), typeof(AxisOption), typeof(BarChartView), default(AxisOption));

        /// <summary>
        /// Gets or sets type of bar chart.
        /// </summary>
        public BarChartType BarChartType
        {
            get { return (BarChartType)GetValue(BarChartTypeProperty); }
            set { SetValue(BarChartTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a bar width.
        /// </summary>
        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of bar top.
        /// </summary>
        public double BarTopRadius
        {
            get { return (double)GetValue(BarTopRadiusProperty); }
            set { SetValue(BarTopRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of bar bottom.
        /// </summary>
        public double BarBottomRadius
        {
            get { return (double)GetValue(BarBottomRadiusProperty); }
            set { SetValue(BarBottomRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the AxisOption.
        /// </summary>
        public AxisOption AxisOption
        {
            get { return (AxisOption)GetValue(AxisOptionProperty); }
            set { SetValue(AxisOptionProperty, value); }
        }
    }
}
