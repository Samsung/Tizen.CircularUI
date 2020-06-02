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
    /// A struct that has a Axis properties.
    /// </summary>
    public struct AxisOption
    {
        /// <summary>
        /// Constructor a new AxisOption structure
        /// </summary>
        public AxisOption(bool itemVisible) : this(itemVisible, itemVisible, itemVisible, itemVisible)
        {
        }

        public AxisOption(bool isVisibleOfMajorAxisLine, bool isVisibleOfMinorAxisLine, bool isVisibleOfReferenceLabel = false, bool isVisibleOfReferenceLine = false)
        {
            IsVisibleOfMajorAxisLine = isVisibleOfMajorAxisLine;
            IsVisibleOfMinorAxisLine = isVisibleOfMinorAxisLine;
            IsVisibleOfReferenceLabel = isVisibleOfReferenceLabel;
            IsVisibleOfReferenceLine = isVisibleOfReferenceLine;
            AxisLineColor = Color.White;
            CategoryLabels = new List<CategoryLabel>();
            ReferenceDataItems = new List<IDataItem>();
        }

        /// <summary>
        /// Gets or sets the visibility of major axis line.
        /// Major axis can be changed to chart orientation. 
        /// If chart is vertical bar chart, major axis is X axis. otherwise(horizontal bar chart), major axis is Y axis.
        /// </summary>
        public bool IsVisibleOfMajorAxisLine { get; set; }

        /// <summary>
        /// Gets or sets the visibility of minor axis line.
        /// Minor axis can be changed to chart orientation. 
        /// If chart is vertical bar chart, minor axis is Y axis. otherwise(horizontal bar chart), minor axis is X axis.
        /// </summary>
        public bool IsVisibleOfMinorAxisLine { get; set; }

        /// <summary>
        /// Gets or sets the visibility of reference label.
        /// Reference label display reference value in minor axis
        /// </summary>
        public bool IsVisibleOfReferenceLabel { get; set; }

        /// <summary>
        /// Gets or sets the visibility of reference line.
        /// </summary>
        public bool IsVisibleOfReferenceLine { get; set; }

        /// <summary>
        /// Gets or sets a color of major & minor axis line.
        /// </summary>
        public Color AxisLineColor { get; set; }

        /// <summary>
        /// Gets or sets a list of category labels.
        /// Category label display data's category in major axis.
        /// For example, If the chart represents a change in the value of a week, each category becomes the day of the week.
        /// </summary>
        public IList<CategoryLabel> CategoryLabels { get; set; }

        /// <summary>
        /// Gets or sets a list of reference DataItem.
        /// </summary>
        public IList<IDataItem> ReferenceDataItems { get; set; }

        public static implicit operator AxisOption(bool uniformValue)
        {
            return new AxisOption(uniformValue);
        }

        bool Equals(AxisOption other)
        {
            if (IsVisibleOfMajorAxisLine != other.IsVisibleOfMajorAxisLine)
                return false;
            if (IsVisibleOfMinorAxisLine != other.IsVisibleOfMinorAxisLine)
                return false;
            if (IsVisibleOfReferenceLabel != other.IsVisibleOfReferenceLabel)
                return false;
            if (IsVisibleOfReferenceLine != other.IsVisibleOfReferenceLine)
                return false;
            if (AxisLineColor != other.AxisLineColor)
                return false;
            if (!CategoryLabels.Equals(other.CategoryLabels))
                return false;
            if (!ReferenceDataItems.Equals(other.ReferenceDataItems))
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is AxisOption axisOption && Equals(axisOption);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsVisibleOfMajorAxisLine.GetHashCode();
                hashCode = (hashCode * 397) ^ IsVisibleOfMinorAxisLine.GetHashCode();
                hashCode = (hashCode * 397) ^ IsVisibleOfReferenceLabel.GetHashCode();
                hashCode = (hashCode * 397) ^ IsVisibleOfReferenceLine.GetHashCode();
                hashCode = (hashCode * 397) ^ AxisLineColor.GetHashCode();
                hashCode = (hashCode * 397) ^ CategoryLabels.GetHashCode();
                hashCode = (hashCode * 397) ^ ReferenceDataItems.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(AxisOption left, AxisOption right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AxisOption left, AxisOption right)
        {
            return !left.Equals(right);
        }

        public void Deconstruct(out bool isVisibleOfMajorAxisLine,
                                out bool isVisibleOfMinorAxisLine,
                                out bool isVisibleOfReferenceLabe,
                                out bool isVisibleOfReferenceLine,
                                out Color axisLineColor,
                                out IList<CategoryLabel> categoryLabels,
                                out IList<IDataItem> referenceDatas
                                )
        {
            isVisibleOfMajorAxisLine = IsVisibleOfMajorAxisLine;
            isVisibleOfMinorAxisLine = IsVisibleOfMinorAxisLine;
            isVisibleOfReferenceLabe = IsVisibleOfReferenceLabel;
            isVisibleOfReferenceLine = IsVisibleOfReferenceLine;
            axisLineColor = AxisLineColor;
            categoryLabels = CategoryLabels;
            referenceDatas = ReferenceDataItems;
        }
    }
}
