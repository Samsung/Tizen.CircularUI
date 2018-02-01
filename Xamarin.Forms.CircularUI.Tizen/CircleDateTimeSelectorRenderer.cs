using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using EDateTimeFieldType = ElmSharp.DateTimeFieldType;
using ECircleDateTimeSelector = ElmSharp.Wearable.CircleDateTimeSelector;

[assembly: ExportRenderer(typeof(Xamarin.Forms.CircularUI.CircleDateTimeSelector), typeof(Xamarin.Forms.CircularUI.Tizen.CircleDateTimeSelectorRenderer))]


namespace Xamarin.Forms.CircularUI.Tizen
{
    public class CircleDateTimeSelectorRenderer : ViewRenderer<CircleDateTimeSelector, ECircleDateTimeSelector>
    {
        public CircleDateTimeSelectorRenderer()
        {
            RegisterPropertyHandler(CircleDateTimeSelector.MarkerColorProperty, UpdateMarkerColor);
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

        protected override Size MinimumSize()
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
            if (null != Control && null != Element && Element.MarkerColor != Color.Default)
            {
                Control.MarkerColor = Element.MarkerColor.ToNative();
            }
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
