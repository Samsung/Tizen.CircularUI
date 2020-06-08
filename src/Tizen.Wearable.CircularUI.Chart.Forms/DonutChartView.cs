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
using Xamarin.Forms.Internals;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// The DonutChartView is a circular chart that shows information as a donut divided into slices. 
    /// </summary>
    public class DonutChartView : PieChartView
    {
        /// <summary>
        /// BindableProperty. Identifies the ThicknessRatio bindable property.
        /// </summary>
        public static readonly BindableProperty ThicknessRatioProperty = BindableProperty.Create(nameof(ThicknessRatio), typeof(double), typeof(DonutChartView), defaultValue: 0.5, coerceValue: (bo, v) => ((double)v).Clamp(0.2, 1));

        /// <summary>
        /// Gets or sets ratio of thickness.
        /// </summary>
        public double ThicknessRatio
        {
            get { return (double)GetValue(ThicknessRatioProperty); }
            set { SetValue(ThicknessRatioProperty, value); }
        }
    }
}
