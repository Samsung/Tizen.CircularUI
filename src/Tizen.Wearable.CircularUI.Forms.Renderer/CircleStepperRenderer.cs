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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native.Watch;
using ElmSharp.Wearable;
using ESize = ElmSharp.Size;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircleStepper), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleStepperRenderer))]


namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleStepperRenderer : StepperRenderer
    {
        public CircleStepperRenderer()
        {
#pragma warning disable CS0618 // MarkerColorProperty and MarkerLineWidthProperty are obsolete
            RegisterPropertyHandler(CircleStepper.MarkerColorProperty, UpdateMarkerColor);
            RegisterPropertyHandler(CircleStepper.MarkerLineWidthProperty, UpdateMarkerLineWidth);
#pragma warning restore CS0618 // MarkerColorProperty and MarkerLineWidthProperty are obsolete
            RegisterPropertyHandler(CircleStepper.LabelFormatProperty, UpdateLabelFormat);
            RegisterPropertyHandler(CircleStepper.TitleProperty, UpdateTitle);
            RegisterPropertyHandler(CircleStepper.IsWrapEnabledProperty, UpdateWrapEnabled);
        }

        protected new WatchSpinner Control => base.Control as WatchSpinner;

        protected new CircleStepper Element => base.Element as CircleStepper;

        protected override void OnElementChanged(ElementChangedEventArgs<Stepper> e)
        {
            base.OnElementChanged(e);

            Control.WheelAppeared += OnListShow;
            Control.WheelDisappeared += OnListHide;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    Control.ValueChanged -= OnValueChanged;
                }
            }
            base.Dispose(disposing);
        }

        protected override Size MinimumSize()
        {
            // This width and height are values taken from SPINNER_LAYOUT_CONTENT_AREA_SIZE of elm-theme-tizen-wearable module
            return new ESize(360, 110).ToDP();
        }

        protected override ESize Measure(int availableWidth, int availableHeight)
        {
            return new ESize(360, 110);
        }

        void OnListShow(object sender, EventArgs args)
        {
            Element.SendWheelAppeared();
        }

        void OnListHide(object sender, EventArgs args)
        {
            Element.SendWheelDisappeared();
        }

        void UpdateMarkerColor()
        {
#pragma warning disable CS0618 // MarkerColor is obsolete
            if (Element.MarkerColor != Color.Default)
            {
                Control.MarkerColor = Element.MarkerColor.ToNative();
            }
#pragma warning restore CS0618 // MarkerColor is obsolete
        }

        void UpdateMarkerLineWidth()
        {
#pragma warning disable CS0618 // MarkerLineWidth is obsolete
            Control.MarkerLineWidth = Element.MarkerLineWidth;
#pragma warning restore CS0618 // MarkerLineWidth is obsolete
        }

        void UpdateLabelFormat()
        {
            Control.LabelFormat = Element.LabelFormat;
        }

        void UpdateTitle()
        {
            Control.SetPartText("elm.text", Element.Title);
        }

        void UpdateIncrement()
        {
            Control.Step = Element.Increment;
        }

        void UpdateWrapEnabled()
        {
            Control.IsWrapEnabled = Element.IsWrapEnabled;
        }
    }
}
