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
    /// The GroupBarChartView is a view that represents vertical/horizontal grouped bar chart.
    /// </summary>
    public class GroupBarChartView : BarChartView
    {
        /// <summary>
        /// BindableProperty. Identifies the GroupBarMargin bindable property.
        /// </summary>
        public static readonly BindableProperty GroupBarMarginProperty = BindableProperty.Create(nameof(GroupBarMargin), typeof(double), typeof(GroupBarChartView), 5d);

        /// <summary>
        /// Gets or sets the horizontal margin between each bar in the grouped bar.
        /// </summary>
        public double GroupBarMargin
        {
            get { return (double)GetValue(GroupBarMarginProperty); }
            set { SetValue(GroupBarMarginProperty, value); }
        }
    }
}
