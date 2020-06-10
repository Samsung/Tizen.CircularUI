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

using System.Collections.Generic;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// The PieChartView is a circular chart that shows information as a pie divided into slices. 
    /// </summary>
    public class PieChartView : ChartView
    {
        /// <summary>
        /// BindableProperty. Identifies the ValueLabelIsVisible bindable property.
        /// </summary>
        public static readonly BindableProperty ValueLabelIsVisibleProperty = BindableProperty.Create(nameof(ValueLabelIsVisible), typeof(bool), typeof(PieChartView), defaultValue: true);

        /// <summary>
        /// BindableProperty. Identifies the ValueLabelFontSize bindable property.
        /// </summary>
        public static readonly BindableProperty ValueLabelFontSizeProperty = BindableProperty.Create(nameof(ValueLabelFontSize), typeof(double), typeof(PieChartView), defaultValue: 5d);

        /// <summary>
        /// BindableProperty. Identifies the ValueLabelColor bindable property.
        /// </summary>
        public static readonly BindableProperty ValueLabelColorProperty = BindableProperty.Create(nameof(ValueLabelColor), typeof(Color), typeof(PieChartView), defaultValue: Color.White);

        /// <summary>
        /// BindableProperty. Identifies the ValueLabelTextFormat bindable property.
        /// </summary>
        public static readonly BindableProperty ValueLabelTextFormatProperty = BindableProperty.Create(nameof(ValueLabelTextFormat), typeof(string), typeof(PieChartView), defaultValue: "{0:P1}");

        /// <summary>
        /// BindableProperty. Identifies the LegendPosition bindable property.
        /// </summary>
        public static readonly BindableProperty LegendIsVisibleProperty = BindableProperty.Create(nameof(LegendIsVisible), typeof(bool), typeof(PieChartView), defaultValue: true);

        /// <summary>
        /// BindableProperty. Identifies the LegendPosition bindable property.
        /// </summary>
        public static readonly BindableProperty LegendPositionProperty = BindableProperty.Create(nameof(LegendPosition), typeof(LegendPosition), typeof(PieChartView), defaultValue: LegendPosition.Bottom);

        /// <summary>
        /// Gets or sets visibility of each slice value.
        /// </summary>
        public bool ValueLabelIsVisible
        {
            get { return (bool)GetValue(ValueLabelIsVisibleProperty); }
            set { SetValue(ValueLabelIsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets size of value label.
        /// </summary>
        public double ValueLabelFontSize
        {
            get { return (double)GetValue(ValueLabelFontSizeProperty); }
            set { SetValue(ValueLabelFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets color of value label.
        /// </summary>
        public Color ValueLabelColor
        {
            get { return (Color)GetValue(ValueLabelColorProperty); }
            set { SetValue(ValueLabelColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets text format of value label.
        /// </summary>
        public string ValueLabelTextFormat
        {
            get { return (string)GetValue(ValueLabelTextFormatProperty); }
            set { SetValue(ValueLabelTextFormatProperty, value); }
        }

        /// <summary>
        /// Gets or sets visibility of legend.
        /// </summary>
        public bool LegendIsVisible
        {
            get { return (bool)GetValue(LegendIsVisibleProperty); }
            set { SetValue(LegendIsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets position of legend.
        /// </summary>
        public LegendPosition LegendPosition
        {
            get { return (LegendPosition)GetValue(LegendPositionProperty); }
            set { SetValue(LegendPositionProperty, value); }
        }
    }
}
