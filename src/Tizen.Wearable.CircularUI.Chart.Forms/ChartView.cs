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
using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    /// <summary>
    /// ChartView is the parent and an abstract class of all chart view.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public abstract class ChartView : View
    {
        /// <summary>
        /// BindableProperty. Identifies the Data bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data), typeof(Data), typeof(ChartView), defaultValue: null);

        /// <summary>
        /// BindableProperty. Identifies the Maximum bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(ChartView), 100.0, validateValue: (bindable, value) =>
        {
            var chart = (ChartView)bindable;
            return (double)value > chart.Minimum;
        });

        /// <summary>
        /// BindableProperty. Identifies the Minimum bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(ChartView), 0.0, validateValue: (bindable, value) =>
        {
            var chart = (ChartView)bindable;
            return (double)value < chart.Maximum;
        });

        /// <summary>
        /// Gets or sets a maximum value of data set.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets a minimum value of data set.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets a value range of data set.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public double ValueRange
        {
            get { return (double)(this.Maximum - this.Minimum); }
        }


        /// <summary>
        /// Gets or sets a list of data set.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Data Data
        {
            get { return (Data)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler DrawChartRequested;

        /// <summary>
        /// Request to update a chart.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void UpdateChart()
        {
            DrawChartRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
