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
using Xamarin.Forms.Platform.Tizen;
using EDateTimeFieldType = ElmSharp.DateTimeFieldType;
using ECircleDateTimeSelector = ElmSharp.Wearable.CircleDateTimeSelector;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircleDateTimeSelector), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleDateTimeSelectorRenderer))]


namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleDateTimeSelectorRenderer : ViewRenderer<CircleDateTimeSelector, ECircleDateTimeSelector>
    {
        public CircleDateTimeSelectorRenderer()
        {
#pragma warning disable CS0618 // MarkerColorProperty is obsolete
            RegisterPropertyHandler(CircleDateTimeSelector.MarkerColorProperty, UpdateMarkerColor);
#pragma warning restore CS0618 // MarkerColorProperty is obsolete
            RegisterPropertyHandler(CircleDateTimeSelector.ValueTypeProperty, UpdateValueType);
            RegisterPropertyHandler(CircleDateTimeSelector.DateTimeProperty, UpdateDateTime);
            RegisterPropertyHandler(CircleDateTimeSelector.MaximumDateProperty, UpdateMaximum);
            RegisterPropertyHandler(CircleDateTimeSelector.MinimumDateProperty, UpdateMinimum);

            RegisterPropertyHandler(CircleDateTimeSelector.IsVisibleOfYearProperty, UpdateFieldVisibilityOfYear);
            RegisterPropertyHandler(CircleDateTimeSelector.IsVisibleOfMonthProperty, UpdateFieldVisibilityOfMonth);
            RegisterPropertyHandler(CircleDateTimeSelector.IsVisibleOfDateProperty, UpdateFieldVisibilityOfDate);
            RegisterPropertyHandler(CircleDateTimeSelector.IsVisibleOfHourProperty, UpdateFieldVisibilityOfHour);
            RegisterPropertyHandler(CircleDateTimeSelector.IsVisibleOfMinuteProperty, UpdateFieldVisibilityOfMinute);
            RegisterPropertyHandler(CircleDateTimeSelector.IsVisibleOfAmPmProperty, UpdateFieldVisibilityOfAmPm);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircleDateTimeSelector> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                if (null != surface)
                {
                    SetNativeControl(new ECircleDateTimeSelector(Xamarin.Forms.Platform.Tizen.Forms.NativeParent, surface));
                    Control.DateTimeChanged += OnDateTimeChanged;
                }
                else
                {
                    throw new CirclePageNotFoundException();
                }
            }
            base.OnElementChanged(e);
        }

        protected override Xamarin.Forms.Size MinimumSize()
        {
            return new ElmSharp.Size(300, 290).ToDP();
        }

        protected override ElmSharp.Size Measure(int availableWidth, int availableHeight)
        {
            return new ElmSharp.Size(300, 290);
        }

        void UpdateMinimum()
        {
            if (null != Control && null != Element)
            {
                Control.MinimumDateTime = Element.MinimumDate;
            }
        }

        void UpdateMaximum()
        {
            if (null != Control && null != Element)
            {
                Control.MaximumDateTime = Element.MaximumDate;
            }
        }

        void UpdateDateTime()
        {
            if (null != Control && null != Element && Element.DateTime != Control.DateTime)
            {
                Control.DateTime = Element.DateTime;
            }
        }

        void UpdateValueType()
        {
            if (null != Control && null != Element)
            {
                if (Element.ValueType == DateTimeType.Date)
                {
                    Control.Style = "datepicker/circle";
                }
                else if (Element.ValueType == DateTimeType.Time)
                {
                    Control.Style = "timepicker/circle";
                    Control.Format = "%d/%b/%Y%I:%M%p";
                }
            }
        }

        void UpdateMarkerColor()
        {
#pragma warning disable CS0618 // MarkerColor is obsolete
            if (null != Control && null != Element && Element.MarkerColor != Xamarin.Forms.Color.Default)
            {
                Control.MarkerColor = Element.MarkerColor.ToNative();
            }
#pragma warning restore CS0618 // MarkerColor is obsolete
        }

        void OnDateTimeChanged(object sender, EventArgs e)
        {
            if (null != Control && null != Element)
            {
                Element.DateTime = Control.DateTime;
            }
        }

        void UpdateFieldVisibilityOfYear()
        {
            if (null != Control && null != Element)
            {
                Control.SetFieldVisible(EDateTimeFieldType.Year, Element.IsVisibleOfYear);
            }
        }

        void UpdateFieldVisibilityOfMonth()
        {
            if (null != Control && null != Element)
            {
                Control.SetFieldVisible(EDateTimeFieldType.Month, Element.IsVisibleOfMonth);
            }
        }

        void UpdateFieldVisibilityOfDate()
        {
            if (null != Control && null != Element)
            {
                Control.SetFieldVisible(EDateTimeFieldType.Date, Element.IsVisibleOfDate);
            }
        }

        void UpdateFieldVisibilityOfHour()
        {
            if (null != Control && null != Element)
            {
                Control.SetFieldVisible(EDateTimeFieldType.Hour, Element.IsVisibleOfHour);
            }
        }

        void UpdateFieldVisibilityOfMinute()
        {
            if (null != Control && null != Element)
            {
                Control.SetFieldVisible(EDateTimeFieldType.Minute, Element.IsVisibleOfMinute);
            }
        }

        void UpdateFieldVisibilityOfAmPm()
        {
            if (null != Control && null != Element)
            {
                Control.SetFieldVisible(EDateTimeFieldType.AmPm, Element.IsVisibleOfAmPm);
            }
        }
    }
}
