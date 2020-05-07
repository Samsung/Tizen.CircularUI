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
    /// <since_tizen> 4 </since_tizen>
    public class BarChartView : ChartView
    {
        /// <summary>
        /// BindableProperty. Identifies the BarChartType bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarChartTypeProperty = BindableProperty.Create(nameof(BarChartType), typeof(BarChartType), typeof(BarChartView), BarChartType.Vertical);

        /// <summary>
        /// BindableProperty. Identifies the BarWidth bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarWidthProperty = BindableProperty.Create(nameof(BarWidth), typeof(double), typeof(BarChartView), 10d);

        /// <summary>
        /// BindableProperty. Identifies the BarBackgroundColorIsVisible bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarBackgroundColorIsVisibleProperty = BindableProperty.Create(nameof(BarBackgroundColorIsVisible), typeof(bool), typeof(BarChartView), false);

        /// <summary>
        /// BindableProperty. Identifies the BarTopRadius bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarTopRadiusProperty = BindableProperty.Create(nameof(BarTopRadius), typeof(double), typeof(BarChartView), 0d);

        /// <summary>
        /// BindableProperty. Identifies the BarBottomRadius bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BarBottomRadiusProperty = BindableProperty.Create(nameof(BarBottomRadius), typeof(double), typeof(BarChartView), 0d);

        /// <summary>
        /// BindableProperty. Identifies the AxisOption bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty AxisOptionProperty = BindableProperty.Create(nameof(AxisOption), typeof(AxisOption), typeof(BarChartView), default(AxisOption));

        /// <summary>
        /// BindableProperty. Identifies the ValueLabelPosition bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ValueLabelPositionProperty = BindableProperty.Create(nameof(ValueLabelPosition), typeof(ValueLabelPosition), typeof(BarChartView), ValueLabelPosition.End);

        /// <summary>
        /// BindableProperty. Identifies the ValueLabelIsVisible bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ValueLabelIsVisibleProperty = BindableProperty.Create(nameof(ValueLabelIsVisible), typeof(bool), typeof(BarChartView), false);

        /// <summary>
        /// Gets or sets type of bar chart.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public BarChartType BarChartType
        {
            get { return (BarChartType)GetValue(BarChartTypeProperty); }
            set { SetValue(BarChartTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a bar width.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of bar background color.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool BarBackgroundColorIsVisible
        {
            get { return (bool)GetValue(BarBackgroundColorIsVisibleProperty); }
            set { SetValue(BarBackgroundColorIsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of bar top.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double BarTopRadius
        {
            get { return (double)GetValue(BarTopRadiusProperty); }
            set { SetValue(BarTopRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of bar bottom.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double BarBottomRadius
        {
            get { return (double)GetValue(BarBottomRadiusProperty); }
            set { SetValue(BarBottomRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the AxisOption.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public AxisOption AxisOption
        {
            get { return (AxisOption)GetValue(AxisOptionProperty); }
            set { SetValue(AxisOptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a position of data value label.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public ValueLabelPosition ValueLabelPosition
        {
            get { return (ValueLabelPosition)GetValue(ValueLabelPositionProperty); }
            set { SetValue(ValueLabelPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of value label.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool ValueLabelIsVisible
        {
            get { return (bool)GetValue(ValueLabelIsVisibleProperty); }
            set { SetValue(ValueLabelIsVisibleProperty, value); }
        }
    }
}
